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

public class Split_info
{
	internal RECT mbr;
	internal RECT mbr2;
	internal float xcut;
	internal float ycut;
	internal node newnode;
	internal float cutxl;
	internal float cutyl;
	internal float cutxh;
	internal float cutyh;
	
	public Split_info()
	{
		mbr = null;
		newnode = null;
		xcut = - 1;
		ycut = - 1;
	}
}