/*
COMP 630C project
Re-implementation of R+ tree

Group member:
Cheng Wing Hang, Nelson
Cheung Kwok Ho, Steven
Ngai Ming Wai, Ryan
Shiu Hoi Nam
Tsui Chi Man*/

/*
A basic extension of the java.awt.Dialog class
*/
using System;

[Serializable]
public class AboutDialog:System.Windows.Forms.Form
{
	
	//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
	public AboutDialog(System.Windows.Forms.Form parent, bool modal):base()
	{
		//UPGRADE_TODO: Constructor 'java.awt.Dialog.Dialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtDialogDialog_javaawtFrame_boolean'"
		SupportClass.DialogSupport.SetDialog(this, parent);
		
		// This code is automatically generated by Visual Cafe when you add
		// components to the visual environment. It instantiates and initializes
		// the components. To modify the code, only use code syntax that matches
		// what Visual Cafe can generate, or Visual Cafe may be unable to back
		// parse your Java file into its visual environment.
		
		//{{INIT_CONTROLS
		//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
		/*
		setLayout(null);*/
		//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
		Size = new System.Drawing.Size(249, 150);
		System.Windows.Forms.Label temp_Label;
		temp_Label = new System.Windows.Forms.Label();
		temp_Label.Text = "A Basic Java  Application";
		label1 = temp_Label;
		label1.SetBounds(40, 35, 166, 21);
		//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
		Controls.Add(label1);
		System.Windows.Forms.Button temp_Button;
		temp_Button = new System.Windows.Forms.Button();
		temp_Button.Text = "OK";
		okButton = temp_Button;
		okButton.SetBounds(95, 85, 66, 27);
		//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
		Controls.Add(okButton);
		Text = "About";
		FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		//}}
		
		//{{REGISTER_LISTENERS
		SymWindow aSymWindow = new SymWindow(this);
		//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
		this.Closing += new System.ComponentModel.CancelEventHandler(aSymWindow.windowClosing);
		SymAction lSymAction = new SymAction(this);
		okButton.Click += new System.EventHandler(lSymAction.actionPerformed);
		SupportClass.CommandManager.CheckCommand(okButton);
		//}}
	}
	
	//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
	public AboutDialog(System.Windows.Forms.Form parent, System.String title, bool modal):this(parent, modal)
	{
		Text = title;
	}
	
	//UPGRADE_NOTE: The equivalent of method 'java.awt.Dialog.addNotify' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
	public void  addNotify()
	{
		// Record the size of the window prior to calling parents addNotify.
		System.Drawing.Size d = Size;
		
		//UPGRADE_ISSUE: Method 'java.awt.Dialog.addNotify' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogaddNotify'"
		base.addNotify();
		
		// Only do this once.
		if (fComponentsAdjusted)
			return ;
		
		// Adjust components according to the insets
		//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
		Size = new System.Drawing.Size(SupportClass.GetInsets(this)[1] + SupportClass.GetInsets(this)[2] + Size.Width, SupportClass.GetInsets(this)[0] + SupportClass.GetInsets(this)[3] + Size.Height);
		System.Windows.Forms.Control[] temp_array;
		AboutDialog temp_container;
		temp_container = this;
		temp_array = new System.Windows.Forms.Control[temp_container.Controls.Count];
		temp_container.Controls.CopyTo(temp_array, 0);
		System.Windows.Forms.Control[] components = temp_array;
		for (int i = 0; i < components.Length; i++)
		{
			System.Drawing.Point p = components[i].Location;
			p.Offset(SupportClass.GetInsets(this)[1], SupportClass.GetInsets(this)[0]);
			//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_javaawtPoint'"
			components[i].Location = p;
		}
		
		// Used for addNotify check.
		fComponentsAdjusted = true;
	}
	
	//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
	new virtual public void  ShowDialog()
	{
		System.Drawing.Rectangle bounds = Parent.Bounds;
		System.Drawing.Rectangle abounds = Bounds;
		
		//UPGRADE_TODO: Method 'java.awt.Component.move' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentmove_int_int'"
		Location = new System.Drawing.Point(bounds.X + (bounds.Width - abounds.Width) / 2, bounds.Y + (bounds.Height - abounds.Height) / 2);
		
		//UPGRADE_TODO: Method 'java.awt.Dialog.show' was converted to 'System.Windows.Forms.Form.ShowDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtDialogshow'"
		base.ShowDialog();
	}
	
	//{{DECLARE_CONTROLS
	internal System.Windows.Forms.Label label1;
	internal System.Windows.Forms.Button okButton;
	//}}
	
	// Used for addNotify redundency check.
	internal bool fComponentsAdjusted = false;
	
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SymWindow' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	internal class SymWindow
	{
		public SymWindow(AboutDialog enclosingInstance)
		{
			InitBlock(enclosingInstance);
		}
		private void  InitBlock(AboutDialog enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private AboutDialog enclosingInstance;
		public AboutDialog Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}
			
		}
		public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs event_Renamed)
		{
			event_Renamed.Cancel = true;
			System.Object object_Renamed = event_sender;
			if (object_Renamed == Enclosing_Instance)
				Enclosing_Instance.AboutDialog_WindowClosing(event_sender, event_Renamed);
		}
	}
	
	internal virtual void  AboutDialog_WindowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs event_Renamed)
	{
		Dispose();
	}
	
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SymAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	internal class SymAction
	{
		public SymAction(AboutDialog enclosingInstance)
		{
			InitBlock(enclosingInstance);
		}
		private void  InitBlock(AboutDialog enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private AboutDialog enclosingInstance;
		public AboutDialog Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}
			
		}
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs event_Renamed)
		{
			System.Object object_Renamed = event_sender;
			if (object_Renamed == Enclosing_Instance.okButton)
				Enclosing_Instance.okButton_Clicked(event_sender, event_Renamed);
		}
	}
	
	internal virtual void  okButton_Clicked(System.Object event_sender, System.EventArgs event_Renamed)
	{
		//{{CONNECTION
		// Clicked from okButton Hide the Dialog
		Dispose();
		//}}
	}
}