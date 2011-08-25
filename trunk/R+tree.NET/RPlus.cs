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

// A class for R+ Tree
public class RPlus
{
	
	internal node head;
	internal int fillfactor;
	
	// constructor function
	public RPlus(int fill)
	{
		this.head = null;
		this.fillfactor = fill;
	}
	
	// Pack the tree
	internal virtual void  pack()
	{
		node tmp;
		RECT rect = this.head.getnodesize();
		
		tmp = this.search(rect);
		
		this.head = this.head.pack(tmp, this.fillfactor);
		return ;
	}
	
	// search for objects overlap window W
	internal virtual node search(RECT W)
	{
		node rs;
		
		rs = new node();
		if (this.head == null)
			System.Console.Out.WriteLine("ERROR: Empty Tree\n");
		else
		{
			rs = this.head.search_int(W);
			if (rs.head != null)
			{
				rs = rs.sort('x', 'a');
				rs.head.filter();
			}
		}
		return rs;
	}
	
	// insert new node to RPlus tree
	public virtual void  insert(MBR obj)
	{
		Split_info sp;
		node tmp;
		MBR mbr;
		
		sp = new Split_info();
		tmp = new node();
		// must insert data object in somewhere in RPlus tree
		// ignore number of nodes inserted
		if (this.head == null)
		{
			this.head = new node();
		}
		sp = this.head.insert_obj(obj, this.fillfactor);
		if (sp.newnode != null)
		{
			mbr = new MBR(sp.mbr);
			tmp.insert(mbr);
			tmp.head.child = sp.newnode;
			if (sp.xcut >= 0 && (tmp.head.current.cutxl == - 1 || sp.xcut > tmp.head.current.cutxl))
				tmp.head.current.cutxl = sp.xcut;
			if (sp.ycut >= 0 && (tmp.head.current.cutyl == - 1 || sp.ycut > tmp.head.current.cutyl))
				tmp.head.current.cutyl = sp.ycut;
			if (tmp.head.child != null)
				tmp.head.child.parent = tmp.head;
			tmp.head.resize();
			
			mbr = new MBR(sp.mbr2);
			tmp.insert(mbr);
			tmp.head.child = head;
			if (sp.xcut >= 0 && (tmp.head.current.cutxh == - 1 || sp.xcut < tmp.head.current.cutxh))
				tmp.head.current.cutxh = sp.xcut;
			if (sp.ycut >= 0 && (tmp.head.current.cutyh == - 1 || sp.ycut < tmp.head.current.cutyh))
				tmp.head.current.cutyh = sp.ycut;
			if (tmp.head.child != null)
				tmp.head.child.parent = tmp.head;
			tmp.head.resize();
			this.head = tmp;
		}
	}
	
	// delete nodes in RPlus tree in delete window
	public virtual void  delete(int oid)
	{
		
		if (head == null)
		{
			System.Console.Out.WriteLine("Empty tree");
		}
		// delete nodes of subtree in delete window
		else
		{
			head.delete(oid);
		}
	}
	
	public virtual void  print()
	{
		System.Console.Out.WriteLine("Start");
		head.recursive_print(0);
	}
}