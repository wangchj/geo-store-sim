/*
COMP 630C project
Re-implementation of R+ tree

Group member:
Cheng Wing Hang, Nelson
Cheung Kwok Ho, Steven
Ngai Ming Wai, Ryan
Shiu Hoi Nam
Tsui Chi Man*/
using System;

public class MBR
{
	
	internal RECT mbr; // Bounding box.
	internal int oid; // Object id.
	internal float cutxl;
	internal float cutyl;
	internal float cutxh;
	internal float cutyh;
	
	
	// Constructor function: The coordinates are randomly generated.
	public MBR(int id)
	{
		RECT ran = new RECT();
		
		this.mbr = ran;
		this.oid = id;
		cutxl = - 1;
		cutyl = - 1;
		cutxh = - 1;
		cutyh = - 1;
	}
	
	public MBR(RECT w, int id)
	{
		this.mbr = w;
		this.oid = id;
		cutxl = - 1;
		cutyl = - 1;
		cutxh = - 1;
		cutyh = - 1;
	}
	
	public MBR(RECT w)
	{
		this.mbr = w;
		cutxl = - 1;
		cutyl = - 1;
		cutxh = - 1;
		cutyh = - 1;
	}
	
	internal MBR(int oid, float xl, float yl, float xh, float yh)
	{
		this.oid = oid;
		mbr = new RECT(xl, yl, xh, yh);
		cutxl = - 1;
		cutyl = - 1;
		cutxh = - 1;
		cutyh = - 1;
	}
	
	// Check if this MBR overlaps with R.
	internal virtual int check_overlap(RECT R)
	{
		if ((R.high[0] <= this.mbr.low[0]) || (R.low[0] >= this.mbr.high[0]) || (R.high[1] <= this.mbr.low[1]) || (R.low[1] >= this.mbr.high[1]))
		{
			return 0;
		}
		return 1;
	}
	
	// Print the minimum bounding box and object ID 
	public virtual void  print()
	{
		System.Console.Out.WriteLine("OID: " + this.oid + "   with mbr: " + "( (" + this.mbr.low[0] + ", " + this.mbr.low[1] + "), (" + this.mbr.high[0] + ", " + this.mbr.high[1] + ")  )");
	}
}