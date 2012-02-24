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

public class LIST
{
	
	internal CELL head; // Head of a list of MBRs
	
	// constructor
	public LIST()
	{
		this.head = null;
	}
	
	// constructor
	public LIST(CELL c)
	{
		CELL ptr, temp_cell;
		MBR temp_mbr;
		
		ptr = c;
		this.head = null;
		while (ptr != null)
		{
			temp_cell = new CELL(ptr.current);
			
			if (this.head == null)
			{
				this.head = temp_cell;
			}
			else
			{
				this.head.prev = temp_cell;
				temp_cell.next = this.head;
				this.head = temp_cell;
			}
			
			ptr = ptr.next;
		}
	}
	
	// insert a cell into the list of MBR
	public virtual void  insertList(CELL newCell)
	{
		if (this.head != null)
		{
			this.head.prev = newCell;
		}
		newCell.next = this.head;
		this.head = newCell;
	}
	
	// print the content of whole list
	public virtual void  print()
	{
		CELL curr_cell;
		
		for (curr_cell = this.head; curr_cell != null; curr_cell = curr_cell.next)
		{
			curr_cell.current.print();
		}
	}
}