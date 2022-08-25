#pragma OPENCL EXTENSION cl_khr_fp64 : enable
kernel void nbody (
    double eps,
	double scaleFactor,
	double gravityFactor,
	double xmax,
	double ymax,
	double zmax,
    double dt,
    global read_only double4* bodyMassPos,
    global double4* bodySpeed,
    global double4* bodyAccel,
    global write_only double4* bodyMassPosNew,
    local double4* pblock)
{
    const double4 dtv = (double4)(dt,dt,dt,0.0);
    int gti = get_global_id(0);
    int ti = get_local_id(0);
 
    int n = get_global_size(0);
    int nt = get_local_size(0);
    int nb = n/nt;
 
    double4 p = bodyMassPos[gti];
    double4 s = bodySpeed[gti];
 
    double4 a = (double4)(0.0,0.0,0.0,0.0);
    for(int jb=0; jb < nb; jb++) {
        pblock[ti] = bodyMassPos[jb*nt+ti];
        barrier(CLK_LOCAL_MEM_FENCE);
        for(int j=0; j<nt; j++){
            double4 p2 = pblock[j];
            double4 d = (p2 - p)/scaleFactor;
            double invr = rsqrt(d.x*d.x + d.y*d.y + d.z*d.z + eps);
            double f = p2.w*invr*invr*invr*gravityFactor;
            a += f*d;
        }
 
        barrier(CLK_LOCAL_MEM_FENCE);
    }
    double4 dv = dtv*a;
    s += dv;
    p += dtv*s + 0.5f*dtv*dv;

	if(p.x<0) {
		p.x = -p.x;
		a.x = -a.x;
		s.x = -s.x;
	}
	else if(p.x>xmax) {
		p.x = 2*xmax-p.x;
		a.x = -a.x;
		s.x = -s.x;
	}
	if(p.y<0) {
		p.y = -p.y;
		a.y = -a.y;
		s.y = -s.y;
	}
	else if(p.y>ymax) {
		p.y = 2*ymax-p.y;
		a.y = -a.y;
		s.y = -s.y;
	}
	if(p.z<0) {
		p.z = -p.z;
		a.z = -a.z;
		s.z = -s.z;
	}
	else if(p.z>zmax) {
		p.z = 2*zmax-p.z;
		a.z = -a.z;
		s.z = -s.z;
	}

    bodyAccel[gti] = a;
    bodyMassPosNew[gti] = p;
    bodySpeed[gti] = s;
}