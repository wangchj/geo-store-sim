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

//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
public class CELL
{
	
	internal MBR current;
	internal CELL next;
	internal CELL prev;
	internal node child;
	
	public CELL()
	{
	}
	
	// constructor function
	public CELL(MBR W)
	{
		this.current = W;
		this.next = null;
		this.prev = null;
		this.child = null;
	}
	
	internal virtual int getlength()
	{
		int count = 0;
		CELL tmp = this.child.head;
		
		while (tmp != null)
		{
			count++;
			tmp = tmp.next;
		}
		return count;
	}
	
	internal virtual int getselflength()
	{
		int count = 0;
		CELL tmp = this;
		
		while (tmp != null)
		{
			count++;
			tmp = tmp.next;
		}
		return count;
	}
	
	// Resize the MBR.
	internal virtual void  resize()
	{
		CELL tmp = this.child.head;
		float xl, yl, xh, yh;
		int count = this.getlength();
		RECT rect;
		
		if (tmp != null)
		{
			xl = tmp.current.mbr.low[0];
			yl = tmp.current.mbr.low[1];
			xh = tmp.current.mbr.high[0];
			yh = tmp.current.mbr.high[1];
			tmp = tmp.next;
			while (tmp != null)
			{
				if (xl > tmp.current.mbr.low[0])
				{
					xl = tmp.current.mbr.low[0];
				}
				if (yl > tmp.current.mbr.low[1])
				{
					yl = tmp.current.mbr.low[1];
				}
				if (xh < tmp.current.mbr.high[0])
				{
					xh = tmp.current.mbr.high[0];
				}
				if (yh < tmp.current.mbr.high[1])
				{
					yh = tmp.current.mbr.high[1];
				}
				tmp = tmp.next;
			}
			this.current.mbr.low[0] = xl;
			this.current.mbr.low[1] = yl;
			this.current.mbr.high[0] = xh;
			this.current.mbr.high[1] = yh;
			if (this.current.cutxl >= 0 && this.current.cutxl > xl)
				this.current.mbr.low[0] = this.current.cutxl;
			if (this.current.cutyl >= 0 && this.current.cutyl > yl)
				this.current.mbr.low[1] = this.current.cutyl;
			if (this.current.cutxh >= 0 && this.current.cutxh < xh)
				this.current.mbr.high[0] = this.current.cutxh;
			if (this.current.cutyh >= 0 && this.current.cutyh < yh)
				this.current.mbr.high[1] = this.current.cutyh;
		}
	}
	
	
	// Filter out the duplicated objects.
	public virtual void  filter()
	{
		
		CELL curr_cell;
		CELL next;
		
		for (curr_cell = this; curr_cell != null; curr_cell = curr_cell.next)
		{
			
			if (curr_cell.next != null)
			{
				next = curr_cell.next;
				while (next != null && next.current.oid == curr_cell.current.oid)
				{
					next = next.next;
				}
				curr_cell.next = next;
				if (curr_cell.next != null)
				{
					curr_cell.next.prev = curr_cell;
				}
			}
		}
	}
	
	// Print the mbr of a cell together with the mbrs in the
	// following cells.
	public virtual void  print()
	{
		
		if (this == null)
		{
			return ;
		}
		else
		{
			this.current.print();
			if (this.next == null)
			{
				return ;
			}
			else
			{
				this.next.print();
				return ;
			}
		}
	}
}