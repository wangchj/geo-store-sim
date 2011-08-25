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

public class node
{
	
	internal CELL head;
	internal CELL parent;
	
	// constructor function
	public node()
	{
		this.head = null;
		this.parent = null;
	}
	
	// Wai: I added this
	
	public node(CELL newHead, CELL newParent)
	{
		CELL tmp, newCell;
		
		this.parent = newParent;
		tmp = newHead;
		while (tmp != null)
		{
			newCell = new CELL(tmp.current);
			newCell.child = tmp.child;
			
			if (this.head != null)
			{
				this.head.prev = newCell;
			}
			newCell.next = this.head;
			this.head = newCell;
			tmp = tmp.next;
		}
	}
	
	// ofcreate creats an overflown node.
	internal virtual void  ofcreate(int ff, int oid)
	{
		
		int counter;
		int total;
		int curr_id;
		
		total = 2 * ff + 1;
		curr_id = oid;
		this.head = null;
		
		// Create a number of cells and insert them to the cell list.
		
		for (counter = 0; counter < total; counter++)
		{
			
			MBR temp_mbr = new MBR(curr_id);
			CELL temp_cell = new CELL(temp_mbr);
			
			temp_cell.next = this.head;
			if (this.head == null)
			{
				this.head = temp_cell;
			}
			else
			{
				this.head.prev = temp_cell;
				this.head = temp_cell;
			}
			
			curr_id++;
		}
	}
	
	
	// Sweep the called node.  axis specifies
	// the axis for sweeping while ff specifies
	// the fill factor.
	internal virtual Cut_info sweep(char axis, int ff)
	{
		
		float curr_cost, min_cost;
		float curr_cut, min_cut; ;
		node new_node1 = new node();
		node new_node2 = new node();
		int len1, len2;
		Cut_info i;
		RECT pmbr;
		
		pmbr = new RECT();
		if (this.parent != null)
		{
			pmbr = this.parent.current.mbr;
		}
		else
		{
			pmbr = this.getnodesize();
		}
		min_cost = - 1f;
		min_cut = - 1f;
		
		
		// Split the cells, which are sorted in ascending order. 
		curr_cut = split_cells(axis, 'a', ff, new_node1, new_node2);
		
		// Get the length of the two cell lists.
		len1 = new_node1.getlength();
		len2 = new_node2.getlength();
		
		if (len1 != 0)
		{
			
			// Evaluate the cost.
			curr_cost = evaluate(axis, new_node1, new_node2, curr_cut, pmbr);
			
			if (min_cost < 0)
			{
				min_cost = curr_cost;
				min_cut = curr_cut;
			}
			else if (curr_cost < min_cost)
			{
				min_cost = curr_cost;
				min_cut = curr_cut;
			}
		}
		
		i = new Cut_info(min_cost, min_cut, new_node1.head, new_node2.head);
		return (i);
	}
	
	// Split the cells of a node into two nodes.  The function returns
	// the point where splitting occurs.
	
	internal virtual float split_cells(char axis, char order, int ff, node new_node1, node new_node2)
	{
		
		int si, finished;
		int counter, temp_counter;
		float curr_cut, max;
		node curr_node;
		CELL curr_cell, temp_cell;
		
		
		// Set the sweeping index.
		if (axis == 'x')
		{
			si = 0;
		}
		else
		{
			si = 1;
		}
		
		
		// Get a node with sorted cells and initialize the
		// variables.
		curr_node = this.sort(axis, order);
		curr_cell = curr_node.head;
		curr_cut = curr_cell.current.mbr.low[si];
		max = curr_cell.current.mbr.high[si];
		
		counter = 0;
		
		if (order == 'a')
		{
			
			finished = 0;
			
			// Put at most ff cells in one node.
			while ((curr_cell != null) && (finished == 0))
			{
				temp_cell = curr_cell;
				temp_counter = 0;
				while (curr_cut == curr_cell.current.mbr.low[si])
				{
					curr_cell = curr_cell.next;
					temp_counter++;
					if (curr_cell == null)
					{
						break;
					}
				}
				
				if (counter + temp_counter <= ff)
				{
					while (temp_cell != curr_cell)
					{
						new_node1.insert(temp_cell.current);
						if (max < temp_cell.current.mbr.high[si])
						{
							max = temp_cell.current.mbr.high[si];
						}
						temp_cell = temp_cell.next;
					}
					counter += temp_counter;
					if (counter == ff)
					{
						finished = 1;
					}
					temp_cell = curr_cell;
				}
				else
				{
					while (temp_cell != curr_cell)
					{
						new_node2.insert(temp_cell.current);
						temp_cell = temp_cell.next;
					}
					finished = 1;
				}
				
				if (curr_cell != null)
				{
					curr_cut = curr_cell.current.mbr.low[si];
				}
			}
			
			// Put the remaining cells in another node.
			while (curr_cell != null)
			{
				new_node2.insert(curr_cell.current);
				curr_cell = curr_cell.next;
			}
			
			curr_cell = new_node1.head;
			
			// Add those cells in the first node that are
			// overlapped with the area covered by the second
			// node to the second node.
			if (new_node2.getlength() != 0)
			{
				while (curr_cell != null)
				{
					if (curr_cut < curr_cell.current.mbr.high[si])
					{
						new_node2.insert(curr_cell.current);
					}
					curr_cell = curr_cell.next;
				}
			}
			
			
			// Change the value of cut if no cut is carried at all.
			if (new_node2.getlength() == 0)
			{
				curr_cut = max;
			}
		}
		
		// Return the partition point.
		return (curr_cut);
	}
	
	// Evaluate the partition cost by considering the areas covered
	// by the new sub-regions.
	internal virtual float evaluate(char axis, node new_node1, node new_node2, float curr_cut, RECT pmbr)
	{
		
		int si, ci;
		float[] min = new float[2];
		float[] max = new float[2];
		float area;
		CELL curr_cell;
		RECT r1, r2;
		
		// Set the sweeping index.
		if (axis == 'x')
		{
			si = 0;
			ci = 1;
		}
		else
		{
			si = 1;
			ci = 0;
		}
		
		if (new_node1.getlength() != 0)
		{
			// Find the coordinates of the mbr of the first sub-region.
			curr_cell = new_node1.head;
			
			// Initialize the coordinates.
			min[si] = curr_cell.current.mbr.low[si];
			min[ci] = curr_cell.current.mbr.low[ci];
			if (curr_cell.current.mbr.high[si] < curr_cut)
			{
				max[si] = curr_cell.current.mbr.high[si];
			}
			else
			{
				max[si] = curr_cut;
			}
			max[ci] = curr_cell.current.mbr.high[ci];
			
			curr_cell = curr_cell.next;
			
			// Update the coordinates of the mbr by considering the
			// cells one by one.
			while (curr_cell != null)
			{
				
				if (curr_cell.current.mbr.low[si] < min[si])
				{
					min[si] = curr_cell.current.mbr.low[si];
				}
				
				if (curr_cell.current.mbr.low[ci] < min[ci])
				{
					min[ci] = curr_cell.current.mbr.low[ci];
				}
				
				if (curr_cell.current.mbr.high[si] >= curr_cut)
				{
					max[si] = curr_cut;
				}
				else if (curr_cell.current.mbr.high[si] > max[si])
				{
					max[si] = curr_cell.current.mbr.high[si];
				}
				
				if (curr_cell.current.mbr.high[ci] > max[ci])
				{
					max[ci] = curr_cell.current.mbr.high[ci];
				}
				
				curr_cell = curr_cell.next;
			}
		}
		else
		{
			min[si] = 0;
			min[ci] = 0;
			max[si] = 0;
			max[si] = 0;
		}
		
		// Calculate the area covered by the first sub-region.
		r1 = new RECT(min[0], min[1], max[0], max[1]);
		r2 = r1.intersect(pmbr);
		area = r2.area();
		
		
		if (new_node2.getlength() != 0)
		{
			
			// Find the coordinates of the mbr of the second sub-region.
			curr_cell = new_node2.head;
			
			// Initialize the coordinates.
			if (curr_cell.current.mbr.low[si] > curr_cut)
			{
				min[si] = curr_cell.current.mbr.low[si];
			}
			else
			{
				min[si] = curr_cut;
			}
			min[ci] = curr_cell.current.mbr.low[ci];
			max[si] = curr_cell.current.mbr.high[si];
			max[ci] = curr_cell.current.mbr.high[ci];
			
			curr_cell = curr_cell.next;
			
			// Update the coordinates of the mbr by considering the
			// cells one by one.
			while (curr_cell != null)
			{
				
				if (curr_cell.current.mbr.low[si] <= curr_cut)
				{
					min[si] = curr_cut;
				}
				else if (curr_cell.current.mbr.low[si] < min[si])
				{
					min[si] = curr_cell.current.mbr.low[si];
				}
				
				if (curr_cell.current.mbr.low[ci] < min[ci])
				{
					min[ci] = curr_cell.current.mbr.low[ci];
				}
				
				if (curr_cell.current.mbr.high[si] > max[si])
				{
					max[si] = curr_cell.current.mbr.high[si];
				}
				
				if (curr_cell.current.mbr.high[ci] > max[ci])
				{
					max[ci] = curr_cell.current.mbr.high[ci];
				}
				
				curr_cell = curr_cell.next;
			}
		}
		else
		{
			min[si] = 0;
			min[ci] = 0;
			max[si] = 0;
			max[ci] = 0;
		}
		
		// Add the area covered by the second region to the
		// previous result.
		
		r1 = new RECT(min[0], min[1], max[0], max[1]);
		r2 = r1.intersect(pmbr);
		area += r2.area();
		
		return (area);
	}
	
	// Function called by to redistribute nodes in the list
	// by Nelson
	internal virtual Split_info redistribute_node(int ff, Cut_info cut)
	{
		node tmp, tmp2;
		Split_info info, info2;
		MBR tmpmbr;
		CELL ptr;
		RECT R1, R2, ns1, ns2;
		RECT parent, par2;
		int check1, check2;
		
		info = new Split_info();
		parent = new RECT();
		if (this.parent != null)
		{
			parent.low[0] = this.parent.current.mbr.low[0];
			parent.low[1] = this.parent.current.mbr.low[1];
			parent.high[0] = this.parent.current.mbr.high[0];
			parent.high[1] = this.parent.current.mbr.high[1];
		}
		else
		{
			parent = this.getnodesize();
		}
		par2 = this.getnodesize();
		tmp = new node();
		tmp2 = new node();
		tmp.parent = this.parent;
		tmp2.parent = this.parent;
		if (cut.dir == 'x')
		{
			R1 = new RECT(parent.low[0], parent.low[1], cut.cut, parent.high[1]);
			R2 = new RECT(cut.cut, parent.low[1], parent.high[0], parent.high[1]);
			info.xcut = cut.cut;
		}
		else
		{
			R1 = new RECT(parent.low[0], parent.low[1], parent.high[0], cut.cut);
			R2 = new RECT(parent.low[0], cut.cut, parent.high[0], parent.high[1]);
			info.ycut = cut.cut;
		}
		
		ptr = this.head;
		while (ptr != null)
		{
			check1 = ptr.current.check_overlap(R1);
			check2 = ptr.current.check_overlap(R2);
			
			if (check1 == 1 && check2 != 1)
			{
				tmp.insert(ptr.current);
				tmp.head.child = ptr.child;
				if (tmp.head.child != null)
					tmp.head.child.parent = tmp.head;
			}
			else if (check1 != 1 && check2 == 1)
			{
				tmp2.insert(ptr.current);
				tmp2.head.child = ptr.child;
				if (tmp2.head.child != null)
					tmp2.head.child.parent = tmp2.head;
			}
			else
			{
				if (ptr.child != null)
				{
					info2 = ptr.child.splitnode(ff, cut);
					tmp.insert(ptr.current);
					tmp.head.child = ptr.child;
					tmp.head.child.parent = tmp.head;
					
					tmpmbr = new MBR(info2.mbr);
					tmp2.insert(tmpmbr);
					tmp2.head.child = info2.newnode;
					tmp2.head.child.parent = tmp2.head;
				}
				else
				{
					tmp.insert(ptr.current);
					tmp.head.child = ptr.child;
					tmp.parent = this.parent;
					
					tmp2.insert(ptr.current);
					tmp2.head.child = ptr.child;
					tmp2.parent = this.parent;
				}
			}
			ptr = ptr.next;
		}
		this.head = tmp.head;
		if (this.parent != null)
		{
			this.parent.current.mbr = R1;
			info.cutxl = this.parent.current.cutxl;
			info.cutyl = this.parent.current.cutyl;
			info.cutxh = this.parent.current.cutxh;
			info.cutyh = this.parent.current.cutyh;
		}
		info.mbr2 = R1;
		info.mbr = R2;
		info.newnode = tmp2;
		return info;
	}
	
	// Split the called node.  "this" object points to one node, return the new node created
	internal virtual Split_info splitnode(int fillfactor, Cut_info input_cut)
	{
		return redistribute_node(fillfactor, input_cut);
	}
	
	internal virtual Split_info splitnode(int fillfactor)
	{
		Cut_info cut, cut2;
		
		
		cut = sweep('x', fillfactor);
		cut.dir = 'x';
		cut2 = sweep('y', fillfactor);
		cut2.dir = 'y';
		
		if (cut.cut == - 1 && cut2.cut == - 1)
		{
			System.Console.Out.WriteLine("Cannot split node!  Terminated!");
			System.Environment.Exit(1);
		}
		else if (cut.cut == - 1)
		{
			if (cut2.S2.head.getselflength() > fillfactor)
			{
				System.Console.Out.WriteLine("Cannot split node!  Terminated!");
				System.Environment.Exit(1);
			}
			return redistribute_node(fillfactor, cut2);
		}
		else if (cut2.cut == - 1)
		{
			if (cut.S2.head.getselflength() > fillfactor)
			{
				System.Console.Out.WriteLine("Cannot split node!  Terminated!");
				System.Environment.Exit(1);
			}
			return redistribute_node(fillfactor, cut);
		}
		else if (cut.cost <= cut2.cost)
		{
			if (cut.S2.head.getselflength() > fillfactor)
			{
				System.Console.Out.WriteLine("Cannot split node!  Terminated!");
				System.Environment.Exit(1);
			}
			return redistribute_node(fillfactor, cut);
		}
		
		if (cut2.S2.head.getselflength() > fillfactor)
		{
			System.Console.Out.WriteLine("Cannot split node!  Terminated!");
			System.Environment.Exit(1);
		}
		return redistribute_node(fillfactor, cut2);
	}
	
	// Partition.  	return partition_info, a new node R and a list of
	//	     	MBRs in S
	
	internal virtual Partition_info partition(node N, int fillfactor)
	{
		Partition_info result;
		Cut_info xsweep, ysweep;
		Cut_info select;
		CELL tmp, cur, cur_cell;
		
		result = new Partition_info();
		result.R.parent = N.parent;
		if (N.getlength() <= fillfactor)
		{
			result.R.head = N.head;
			return result;
		}
		xsweep = N.sweep('x', fillfactor);
		ysweep = N.sweep('y', fillfactor);
		
		cur = new CELL();
		select = new Cut_info();
		if (xsweep.cost == - 1 && ysweep.cost == - 1)
		{
			System.Console.Out.WriteLine("ERROR : Cannot Split node!!");
			System.Environment.Exit(1);
		}
		else if (xsweep.cost == - 1 || (xsweep.cost > ysweep.cost && ysweep.cost != - 1))
		{
			select = ysweep;
			result.xy = 'y';
		}
		else
		{
			select = xsweep;
			result.xy = 'x';
		}
		
		//	create the new node R
		cur = select.S1.head;
		
		while (cur != null)
		{
			cur_cell = new CELL(cur.current);
			
			//	find the child node
			tmp = N.head;
			if (tmp != null)
			{
				while (tmp.current.mbr.low[0] != cur_cell.current.mbr.low[0] || tmp.current.mbr.low[1] != cur_cell.current.mbr.low[1] || tmp.current.mbr.high[0] != cur_cell.current.mbr.high[0] || tmp.current.mbr.high[1] != cur_cell.current.mbr.high[1])
				{
					tmp = tmp.next;
				}
				
				cur_cell.child = tmp.child;
				cur_cell.current = tmp.current;
				
				//      insert into the node
				if (result.R.head != null)
				{
					result.R.head.prev = cur_cell;
					cur_cell.next = result.R.head;
				}
				result.R.head = cur_cell;
			}
			cur = cur.next;
		}
		
		//      create the list S
		cur = select.S2.head;
		while (cur != null)
		{
			cur_cell = new CELL(cur.current);
			cur_cell.child = cur.child;
			result.S.insertList(cur_cell);
			cur = cur.next;
		}
		if (result.xy == 'x')
		{
			result.xcut = select.cut;
		}
		else if (result.xy == 'y')
		{
			result.ycut = select.cut;
		}
		return result;
	}
	
	//  Delete W from the list of cells in leaf node.
	internal virtual int delete_leaf(int oid)
	{
		CELL curr_cell;
		
		if (this != null)
		{
			for (curr_cell = this.head; curr_cell != null; curr_cell = curr_cell.next)
			{
				
				if (curr_cell.current.oid == oid)
				{
					this.delOneCell(curr_cell);
					return (1);
				}
			}
			return (0);
		}
		else
		{
			System.Console.Out.WriteLine("Error: Null leaf node.");
			return (0);
		}
	}
	
	
	// attach new child to node
	internal virtual void  attach(MBR obj)
	{
		CELL new_cell; // new cell inserted to list
		
		// create new cell
		new_cell = new CELL(obj);
		
		// insert into list of cells
		new_cell.next = head;
		if (head == null)
		{
			head = new_cell;
		}
		else
		{
			head.prev = new_cell;
			head = new_cell;
		}
	}
	
	internal virtual void  insert(MBR W)
	{
		CELL ins_cell;
		
		ins_cell = new CELL(W);
		ins_cell.next = this.head;
		this.head = ins_cell;
		if (this.head.next != null)
			this.head.next.prev = this.head;
	}
	
	// insert new node to subtree of this node
	// return number of nodes inserted
	internal virtual Split_info insert_obj(MBR obj, int fillfactor)
	{
		int counter = 0; // number of nodes inserted
		Split_info sp;
		CELL tmp;
		MBR tmpmbr;
		
		sp = new Split_info();
		// try to insert data object to children
		for (CELL curr_cell = head; curr_cell != null; curr_cell = curr_cell.next)
		{
			// check children for overlapping with data object
			if (curr_cell.current.check_overlap(obj.mbr) == 1 && curr_cell.current.oid == 0)
			{
				// check if curr_cell is data cell
				if (curr_cell.child != null)
				{
					// non-data node
					// insert into subtree
					sp = curr_cell.child.insert_obj(obj, fillfactor);
					if (sp.newnode != null)
					{
						curr_cell.current.mbr = sp.mbr2;
						if (sp.xcut >= 0 && (curr_cell.current.cutxh == - 1 || sp.xcut < curr_cell.current.cutxh))
							curr_cell.current.cutxh = sp.xcut;
						if (sp.ycut >= 0 && (curr_cell.current.cutyh == - 1 || sp.ycut < curr_cell.current.cutyh))
							curr_cell.current.cutyh = sp.ycut;
						curr_cell.resize();
						tmpmbr = new MBR(sp.mbr);
						this.insert(tmpmbr);
						this.head.child = sp.newnode;
						this.head.child.parent = this.head;
						this.head.current.cutxl = sp.cutxl;
						this.head.current.cutyl = sp.cutyl;
						this.head.current.cutxh = sp.cutxh;
						this.head.current.cutyh = sp.cutyh;
						
						if (sp.xcut >= 0 && (this.head.current.cutxl == - 1 || sp.xcut > this.head.current.cutxl))
							this.head.current.cutxl = sp.xcut;
						if (sp.ycut >= 0 && (this.head.current.cutyl == - 1 || sp.ycut > this.head.current.cutyl))
							this.head.current.cutyl = sp.ycut;
						this.head.resize();
					}
					else
					{
						curr_cell.resize();
					}
					counter++;
				}
			}
		}
		
		sp = new Split_info();
		if (counter == 0)
		{
			// attach new child to this node
			attach(obj);
		}
		if (this.getlength() > fillfactor)
		{
			sp = this.splitnode(fillfactor);
		}
		if (this.parent != null)
		{
			this.parent.resize();
		}
		return sp;
	}
	
	// delete nodes of subtree in delete window
	internal virtual void  delete(int oid)
	{
		int counter = 0; // number of nodes deleted
		
		if (this != null)
		{
			// check each children against delete window
			for (CELL curr_cell = this.head; curr_cell != null; curr_cell = curr_cell.next)
			{
				
				if (curr_cell.child != null)
				{
					// search subtree
					curr_cell.child.delete(oid);
					if (curr_cell.child.head == null)
					{
						if (curr_cell.prev != null)
						{
							curr_cell.prev.next = curr_cell.next;
						}
						else
						{
							head = curr_cell.next;
						}
						if (curr_cell.next != null)
						{
							curr_cell.next.prev = curr_cell.prev;
						}
					}
				}
				else
				{
					// search leaf
					counter += delete_leaf(oid);
				}
			}
			if (counter > 0)
			{
				// some children deleted
				if (parent != null)
				{
					// resize MBR of parent cell
					parent.resize();
				}
			}
		}
		else
		{
			System.Console.Out.WriteLine("Error: Null node.");
		}
	}
	
	
	//  Delete the first occurence of W from a cell
	internal virtual void  delOneCell(CELL C)
	{
		if (C == this.head)
			this.head = C.next;
		else
			C.prev.next = C.next;
		
		if (C.next != null)
			C.next.prev = C.prev;
		
		C.next = null;
		C.prev = null;
	}
	
	
	// Sort the MBRs along the given axis.
	internal virtual node sort(char axis, char order)
	{
		
		CELL pt, pt2;
		CELL temp_cell;
		node temp_node, result;
		MBR targetmbr;
		
		temp_node = new node();
		temp_cell = this.head;
		
		
		// Copy the cells to a temporary list.
		while (temp_cell != null)
		{
			temp_node.insert(temp_cell.current);
			temp_cell = temp_cell.next;
		}
		
		result = new node();
		
		if (order == 'a')
		{
			
			// A selection sort is applied.
			while (temp_node.head != null)
			{
				targetmbr = temp_node.head.current;
				
				pt = temp_node.head;
				pt2 = pt;
				
				while (pt != null)
				{
					if (axis == 'x' && pt.current.mbr.low[0] > targetmbr.mbr.low[0])
					{
						targetmbr = pt.current;
						pt2 = pt;
					}
					else if (axis == 'y' && pt.current.mbr.low[1] > targetmbr.mbr.low[1])
					{
						targetmbr = pt.current;
						pt2 = pt;
					}
					pt = pt.next;
				}
				
				if (pt2 == temp_node.head)
				{
					temp_node.head = temp_node.head.next;
				}
				else
				{
					pt2.prev.next = pt2.next;
				}
				if (pt2.next != null)
				{
					pt2.next.prev = pt2.prev;
				}
				result.insert(targetmbr);
			}
		}
		else if (order == 'd')
		{
			
			// A selection sort is applied.
			while (temp_node.head != null)
			{
				targetmbr = temp_node.head.current;
				pt = temp_node.head;
				pt2 = pt;
				
				while (pt != null)
				{
					if (axis == 'x' && pt.current.mbr.high[0] < targetmbr.mbr.high[0])
					{
						targetmbr = pt.current;
						pt2 = pt;
					}
					else if (axis == 'y' && pt.current.mbr.high[1] < targetmbr.mbr.high[1])
					{
						targetmbr = pt.current;
						pt2 = pt;
					}
					pt = pt.next;
				}
				
				if (pt2 == temp_node.head)
				{
					temp_node.head = temp_node.head.next;
				}
				else
				{
					pt2.prev.next = pt2.next;
				}
				if (pt2.next != null)
				{
					pt2.next.prev = pt2.prev;
				}
				result.insert(targetmbr);
			}
		}
		
		return (result);
	}
	
	
	// Return the number of cell in node.
	internal virtual int getlength()
	{
		CELL tmp = this.head;
		int len = 0;
		
		while (tmp != null)
		{
			len++;
			tmp = tmp.next;
		}
		return len;
	}
	
	// Concate 2 nodes (MBR list) together 
	internal virtual void  concate(node concat_node)
	{
		CELL tail;
		
		if (this.head == null)
		{
			this.head = concat_node.head;
			return ;
		}
		if (concat_node.head == null)
			return ;
		
		tail = this.head;
		while (tail.next != null)
			tail = tail.next;
		tail.next = concat_node.head;
		concat_node.head.prev = tail;
	}
	
	// Search all the MBR in a leaf node that overlaps with the query window W.
	internal virtual node search_leaf(RECT W)
	{
		node rs;
		CELL curr_cell;
		MBR new_mbr;
		
		if ((this.head != null) && (this.head.child == null))
		// leaf
		{
			rs = new node();
			curr_cell = this.head;
			
			while (curr_cell != null)
			{
				if (curr_cell.current.check_overlap(W) == 1)
				{
					new_mbr = new MBR(curr_cell.current.mbr, curr_cell.current.oid);
					rs.insert(new_mbr);
				}
				curr_cell = curr_cell.next;
			}
			
			return rs;
		}
		else
		{
			System.Console.Out.WriteLine("ERROR: Empty MBR List or Not Leaf Node\n");
			return null;
		}
	}
	
	//  Recursive search for a given query window W that overlap with 
	//  the intermediate node of a R+ tree
	internal virtual node search_int(RECT W)
	{
		node rs, curr_node;
		CELL curr_cell;
		
		rs = new node();
		if (this.head.child == null)
		// leaf
		{
			rs = this.search_leaf(W);
			return rs;
		}
		else
		{
			curr_cell = this.head;
			while (curr_cell != null)
			{
				if (curr_cell.current.check_overlap(W) == 1)
				{
					rs.concate(curr_cell.child.search_int(W));
				}
				curr_cell = curr_cell.next;
			}
			return rs;
		}
	}
	
	internal virtual void  print()
	{
		CELL curr_cell;
		
		curr_cell = this.head;
		while (curr_cell != null)
		{
			curr_cell.current.print();
			curr_cell = curr_cell.next;
		}
	}
	
	public virtual void  recursive_print(int lvl)
	{
		CELL cur_cell;
		node cur_node;
		
		cur_cell = this.head;
		
		if (this != null)
		{
			while (cur_cell != null)
			{
				System.Console.Out.Write("LEVEL " + lvl + " ");
				cur_cell.current.print();
				if (cur_cell.child != null)
				{
					cur_cell.child.recursive_print(lvl + 1);
				}
				cur_cell = cur_cell.next;
			}
		}
	}
	
	internal virtual RECT getnodesize()
	{
		CELL ptr = this.head;
		float[] low = new float[2];
		float[] high = new float[2];
		RECT rect;
		
		rect = new RECT();
		if (ptr != null)
		{
			rect = new RECT(ptr.current.mbr.low[0], ptr.current.mbr.low[1], ptr.current.mbr.high[0], ptr.current.mbr.high[1]);
			ptr = ptr.next;
			while (ptr != null)
			{
				if (ptr.current.mbr.low[0] < rect.low[0])
					rect.low[0] = ptr.current.mbr.low[0];
				if (ptr.current.mbr.low[1] < rect.low[1])
					rect.low[1] = ptr.current.mbr.low[1];
				if (ptr.current.mbr.high[0] > rect.high[0])
					rect.high[0] = ptr.current.mbr.high[0];
				if (ptr.current.mbr.high[1] > rect.high[1])
					rect.high[1] = ptr.current.mbr.high[1];
				ptr = ptr.next;
			}
		}
		return rect;
	}
	
	internal virtual node pack(node input, int fillfactor)
	{
		node result;
		Partition_info info;
		MBR mbr;
		RECT rect;
		float xcut = 0, ycut = 0;
		
		result = new node();
		while (input.head != null)
		{
			info = new Partition_info();
			info = this.partition(input, fillfactor);
			rect = new RECT(info.R.getnodesize());
			mbr = new MBR(rect);
			result.insert(mbr);
			if (result.head.current.mbr.low[0] < xcut && xcut > 0)
			{
				result.head.current.mbr.low[0] = xcut;
				result.head.current.cutxl = xcut;
			}
			if (result.head.current.mbr.low[1] < ycut && ycut > 0)
			{
				result.head.current.mbr.low[1] = ycut;
				result.head.current.cutyl = ycut;
			}
			if (result.head.current.mbr.high[0] > info.xcut && info.xy == 'x')
			{
				result.head.current.mbr.high[0] = info.xcut;
				result.head.current.cutxh = info.xcut;
			}
			if (result.head.current.mbr.high[1] > info.ycut && info.xy == 'y')
			{
				result.head.current.mbr.high[1] = info.ycut;
				result.head.current.cutyh = info.ycut;
			}
			if (info.xy == 'x')
			{
				xcut = info.xcut;
			}
			if (info.xy == 'y')
			{
				ycut = info.ycut;
			}
			result.head.child = info.R;
			input.head = info.S.head;
		}
		if (result.getlength() <= fillfactor)
		{
			return result;
		}
		
		return this.pack(result, fillfactor);
	}
}