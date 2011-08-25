using System;
//UPGRADE_TODO: The type 'java.util.Scanner' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Scanner = java.util.Scanner;
using System.IO;

public class TEST2
{
	[STAThread]
	public static void  Main(System.String[] argv)
	{
		
		RPlus root;
		RECT rec, s_rec;
		MBR[] obj = new MBR[50];
		int i;
		//UPGRADE_TODO: Constructor 'java.io.FileReader.FileReader' was converted to 'System.IO.StreamReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		System.IO.StreamReader input = new System.IO.StreamReader("roads.txt", System.Text.Encoding.Default);
		//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
		System.IO.StreamReader bufRead = new System.IO.StreamReader(input.BaseStream, input.CurrentEncoding);
		// Case 6: Randomly generate 5 objects.
		System.String[] OID = new System.String[40000];
		System.String[] X1 = new System.String[40000];
		System.String[] X2 = new System.String[40000];
		System.String[] Y1 = new System.String[40000];
		System.String[] Y2 = new System.String[40000];
		int[] iOID = new int[40000];
		int[] iX1 = new int[40000];
		int[] iX2 = new int[40000];
		int[] iY1 = new int[40000];
		int[] iY2 = new int[40000];
		System.String line;
		line = bufRead.ReadLine();
		int num = 0;
		
		while (line != null)
		{
			
			int currentIndex = 0;
			System.String substring = "";
			bool space1 = false;
			bool space2 = false;
			bool space3 = false;
			bool space4 = false;
			while (space1 == false)
			{
				if (line[currentIndex] == ' ')
				{
					OID[num] = substring;
					iOID[num] = System.Int32.Parse(OID[num]);
					space1 = true;
					currentIndex++;
					substring = "";
				}
				else
				{
					substring += line[currentIndex];
					currentIndex++;
				}
			}
			while (space2 == false)
			{
				if (line[currentIndex] == ' ')
				{
					X1[num] = substring;
					iX1[num] = System.Int32.Parse(X1[num]);
					
					space2 = true;
					currentIndex++;
					substring = "";
				}
				else
				{
					substring += line[currentIndex];
					currentIndex++;
				}
			}
			while (space3 == false)
			{
				if (line[currentIndex] == ' ')
				{
					X2[num] = substring;
					iX2[num] = System.Int32.Parse(X2[num]);
					space3 = true;
					currentIndex++;
					substring = "";
				}
				else
				{
					substring += line[currentIndex];
					currentIndex++;
				}
			}
			while (space4 == false)
			{
				if (line[currentIndex] == ' ')
				{
					Y1[num] = substring;
					iY1[num] = System.Int32.Parse(Y1[num]);
					space4 = true;
					currentIndex++;
					substring = "";
				}
				else
				{
					substring += line[currentIndex];
					currentIndex++;
				}
			}
			Y2[num] = line.Substring(currentIndex, (line.Length) - (currentIndex));
			iY2[num] = System.Int32.Parse(Y2[num]);
			num++;
			line = bufRead.ReadLine();
		}
		// For checking accuraccy of input
		/*
		for(int j = 0; j < num; j++){
		System.out.println("OID: " + iOID[j] + " X1: " + iX1[j] + " X2: " + iX2[j] + " Y1: " + iY1[j] + " Y2: " + iY2[j] + ".");
		
		}
		*/
		
		int fillfactor = 31;
		root = new RPlus(fillfactor);
		for (int j = 1; j < 10000; j++)
		{
            Console.WriteLine(j);
			rec = new RECT(iX1[j], iX2[j], iY1[j], iY2[j]);
			obj[1] = new MBR(rec, iOID[j]);
			root.insert(obj[1]);
		}
		/*
		rec = new RECT(4, 7, 4, 7);
		obj[1] = new MBR(rec, 1);
		rec = new RECT(10, 8, 10, 8);
		obj[2] = new MBR(rec, 2);
		rec = new RECT(6, 5, 6, 5);
		obj[3] = new MBR(rec, 3);
		rec = new RECT(3,9, 3, 9);
		obj[4] = new MBR(rec, 4);
		rec = new RECT(7, 6, 7, 6);
		obj[5] = new MBR(rec, 5);
		rec = new RECT(5,3,5,3);
		obj[6] = new MBR(rec, 6);
		rec = new RECT(8,2,8,2);
		obj[7] = new MBR(rec, 7);
		rec = new RECT(8,3,8,3);
		obj[8] = new MBR(rec, 8);
		
		root.insert(obj[1]);
		root.insert(obj[2]);
		root.insert(obj[3]);
		root.insert(obj[4]);
		root.insert(obj[5]);
		root.insert(obj[6]);
		root.insert(obj[7]);
		root.insert(obj[8]);
		//            root.pack();
		*/
		
		
		
		root.print();

        Console.ReadLine();
	}
}