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

// Partition_info:  Stores the returned node and a set of MBRs from partition
public class Partition_info
{
	
	internal node R; // a new node created by partition
	internal LIST S; // Head of a list of MBRs
	internal float cut; // the cut value
	internal char xy; // showing an x-cut or y-cut
	internal float xcut;
	internal float ycut;
	
	public Partition_info()
	{
		this.R = new node();
		this.S = new LIST();
		xcut = - 1;
		ycut = - 1;
	}
}