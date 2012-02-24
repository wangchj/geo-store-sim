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

public class TEST
{
	[STAThread]
	public static void  Main(System.String[] argv)
	{
		
		RPlus root;
		RECT rec, s_rec;
		MBR[] obj = new MBR[6];
		int i;
		
		// Case 6: Randomly generate 5 objects.
		int fillfactor = 3;
		root = new RPlus(fillfactor);
		rec = new RECT(16, 51, 39, 59);
		obj[1] = new MBR(rec, 1);
		rec = new RECT(53, 12, 65, 24);
		obj[2] = new MBR(rec, 2);
		rec = new RECT(4, 46, 11, 64);
		obj[3] = new MBR(rec, 3);
		rec = new RECT(15, 6, 61, 75);
		obj[4] = new MBR(rec, 4);
		rec = new RECT(51, 2, 70, 79);
		obj[5] = new MBR(rec, 5);
		root.insert(obj[1]);
		root.insert(obj[2]);
		root.insert(obj[3]);
		root.insert(obj[4]);
		root.insert(obj[5]);
		//                root.pack();
		root.print();
	}
}