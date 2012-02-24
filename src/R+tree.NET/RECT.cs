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

public class RECT
{
	
	internal float[] low = new float[2]; // Coordinates of lower left corner.
	internal float[] high = new float[2]; // Coordinates of upper right corner.
	
	// constructor function: The coordinates are generated randomly.
	public RECT()
	{
		
		System.Random rg = new System.Random();
		
		int limit = 101; // Maximum value of a coordinate.
		int temp1, temp2; // Dummy variables.
		int counter; // Counter in a for-loop.
		int max_loop = 4096; // The maximum number of loops.
		
		
		// A loop to make sure that the seed (i.e. time) is new.
		for (counter = 0; counter < max_loop; counter++)
		{
			//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.util.Random.Random'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
			//UPGRADE_TODO: Method 'java.util.Random.nextInt' was converted to 'System.Random.Next' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Random rg2 = new System.Random((System.Int32) rg.Next());
			rg2 = rg;
		}
		
		
		// Generate two random numbers.
		//UPGRADE_TODO: Method 'java.util.Random.nextInt' was converted to 'System.Random.Next' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		temp1 = rg.Next() % limit;
		if (temp1 < 0)
		{
			temp1 *= (- 1);
		}
		//UPGRADE_TODO: Method 'java.util.Random.nextInt' was converted to 'System.Random.Next' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		temp2 = rg.Next() % limit;
		if (temp2 < 0)
		{
			temp2 *= (- 1);
		}
		
		
		// Make sure that the two numbers are different.
		if (temp1 == temp2)
		{
			temp2 = (temp2 + 1) % limit;
		}
		
		
		// Fill the x-coordinates.
		if (temp1 < temp2)
		{
			this.low[0] = temp1;
			this.high[0] = temp2;
		}
		else
		{
			this.low[0] = temp2;
			this.high[0] = temp1;
		}
		
		
		// Generate another two random numbers. 
		//UPGRADE_TODO: Method 'java.util.Random.nextInt' was converted to 'System.Random.Next' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		temp1 = rg.Next() % limit;
		if (temp1 < 0)
		{
			temp1 *= (- 1);
		}
		//UPGRADE_TODO: Method 'java.util.Random.nextInt' was converted to 'System.Random.Next' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		temp2 = rg.Next() % limit;
		if (temp2 < 0)
		{
			temp2 *= (- 1);
		}
		
		
		// Again make sure that the two numbers are different.
		if (temp1 == temp2)
		{
			temp2 = (temp2 + 1) % limit;
		}
		
		
		// Fill the y-coordinates.
		if (temp1 < temp2)
		{
			this.low[1] = temp1;
			this.high[1] = temp2;
		}
		else
		{
			this.low[1] = temp2;
			this.high[1] = temp1;
		}
	}
	
	public RECT(RECT rect)
	{
		this.low[0] = rect.low[0];
		this.high[0] = rect.high[0];
		this.low[1] = rect.low[1];
		this.high[1] = rect.high[1];
	}
	
	public RECT(float xl, float yl, float xh, float yh)
	{
		this.low[0] = xl;
		this.low[1] = yl;
		this.high[0] = xh;
		this.high[1] = yh;
	}
	
	// Check whether the given rectangle intersect with current one
	public virtual RECT intersect(RECT r)
	{
		float low_x, low_y, high_x, high_y;
		RECT result;
		
		if (this.low[0] < r.low[0])
		{
			low_x = r.low[0];
		}
		else
		{
			low_x = this.low[0];
		}
		if (this.high[0] > r.high[0])
		{
			high_x = r.high[0];
		}
		else
		{
			high_x = this.high[0];
		}
		if (this.low[1] < r.low[1])
		{
			low_y = r.low[1];
		}
		else
		{
			low_y = this.low[1];
		}
		if (this.high[1] > r.high[1])
		{
			high_y = r.high[1];
		}
		else
		{
			high_y = this.high[1];
		}
		
		result = new RECT(low_x, low_y, high_x, high_y);
		return result;
	}
	
	// Return the area of the rectangle
	public virtual float area()
	{
		return ((high[1] - low[1]) * (high[0] - low[0]));
	}
	
	// Print the coordinate of the Rectangle 
	public virtual void  print()
	{
		System.Console.Out.WriteLine("(  (" + this.low[0] + ", " + this.low[1] + "), (" + this.high[0] + ", " + this.high[1] + ")  )");
	}
}