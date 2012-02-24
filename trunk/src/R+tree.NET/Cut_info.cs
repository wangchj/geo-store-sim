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

// Storing the information after calculation of how to split
// the node and its cost
public class Cut_info
{
	
	internal float cost; // Partition cost.
	internal float cut; // Point where partition takes place.
	internal LIST S1, S2;
	internal char dir;
	
	
	public Cut_info()
	{
	}
	
	// constructor
	public Cut_info(float cost, float cut)
	{
		this.cost = cost;
		this.cut = cut;
	}
	
	// constructor
	public Cut_info(float cost, float cut, CELL C1, CELL C2)
	{
		this.cost = cost;
		this.cut = cut;
		S1 = new LIST(C1);
		S2 = new LIST(C2);
	}
	
	// print where the cut takes place and its cost
	public virtual void  print()
	{
		System.Console.Out.WriteLine("Cost: " + this.cost + ", cut: " + this.cut);
	}
}