//
// In order to convert some functionality to Visual C#, the Java Language Conversion Assistant
// creates "support classes" that duplicate the original functionality.  
//
// Support classes replicate the functionality of the original code, but in some cases they are 
// substantially different architecturally. Although every effort is made to preserve the 
// original architecture of the application in the converted project, the user should be aware that 
// the primary goal of these support classes is to replicate functionality, and that at times 
// the architecture of the resulting solution may differ somewhat.
//

using System;

/// <summary>
/// Contains conversion support elements such as classes, interfaces and static methods.
/// </summary>
public class SupportClass
{
	/// <summary>
	/// Support class for creation of Forms behaving like Dialog windows.
	/// </summary>
	public class DialogSupport
	{
		/// <summary>
		/// Creates a dialog Form.
		/// </summary>
		/// <returns>The new dialog Form instance.</returns>
		public static System.Windows.Forms.Form CreateDialog()
		{
			System.Windows.Forms.Form tempForm = new System.Windows.Forms.Form();
			tempForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			tempForm.ShowInTaskbar = false;
			return tempForm;
		}

		/// <summary>
		/// Sets dialog like properties to a Form.
		/// </summary>
		/// <param name="formInstance">Form instance to be modified.</param>
		public static void SetDialog(System.Windows.Forms.Form formInstance)
		{
			formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			formInstance.ShowInTaskbar = false;
		}

		/// <summary>
		/// Creates a dialog Form with an owner.
		/// </summary>
		/// <param name="owner">The form to be set as owner of the newly created one.</param>
		/// <returns>A new dialog Form.</returns>
		public static System.Windows.Forms.Form CreateDialog(System.Windows.Forms.Form owner)
		{
			System.Windows.Forms.Form tempForm = new System.Windows.Forms.Form();
			tempForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			tempForm.ShowInTaskbar = false;
			tempForm.Owner = owner;
			return tempForm;
		}

		/// <summary>
		/// Sets dialog like properties and an owner to a Form.
		/// </summary>
		/// <param name="formInstance">Form instance to be modified.</param>
		/// <param name="owner">The form to be set as owner of the newly created one.</param>
		public static void SetDialog(System.Windows.Forms.Form formInstance, System.Windows.Forms.Form owner)
		{
			formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			formInstance.ShowInTaskbar = false;
			formInstance.Owner = owner;
		}

		
		/// <summary>
		/// Creates a dialog Form with an owner and a text property.
		/// </summary>
		/// <param name="owner">The form to be set as owner of the newly created one.</param>
		/// <param name="text">The title text for the form.</param>
		/// <returns>The new dialog Form.</returns>
		public static System.Windows.Forms.Form CreateDialog(System.Windows.Forms.Form owner, System.String text)
		{
			System.Windows.Forms.Form tempForm = new System.Windows.Forms.Form();
			tempForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			tempForm.ShowInTaskbar = false;
			tempForm.Owner = owner;
			tempForm.Text = text;
			return tempForm;
		}
				
		/// <summary>
		/// Sets dialog like properties, an owner and a text property to a Form.
		/// </summary>
		/// <param name="formInstance">Form instance to be modified.</param>
		/// <param name="owner">The form to be set as owner of the newly created one.</param>
		/// <param name="text">The title text for the form.</param>
		public static void SetDialog(System.Windows.Forms.Form formInstance, System.Windows.Forms.Form owner, System.String text)
		{
			formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			formInstance.ShowInTaskbar = false;
			formInstance.Owner = owner;
			formInstance.Text = text;
		}

			
		/// <summary>
		/// This method sets or unsets a resizable border style to a Form.
		/// </summary>
		/// <param name="formInstance">The form to be modified.</param>
		/// <param name="sizable">Boolean value to be set.</param>
		public static void SetSizable(System.Windows.Forms.Form formInstance, bool sizable)
		{
			if (sizable)
			{
				formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			}
			else
			{
				formInstance.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			}
		}
	}


	/*******************************/
	/// <summary>
	/// Class used to store and retrieve an object command specified as a String.
	/// </summary>
	public class CommandManager
	{
		/// <summary>
		/// Private Hashtable used to store objects and their commands.
		/// </summary>
		private static System.Collections.Hashtable Commands = new System.Collections.Hashtable();

		/// <summary>
		/// Sets a command to the specified object.
		/// </summary>
		/// <param name="obj">The object that has the command.</param>
		/// <param name="cmd">The command for the object.</param>
		public static void SetCommand(System.Object obj, System.String cmd)
		{
			if (obj != null)
			{
				if (Commands.Contains(obj))
					Commands[obj] = cmd;
				else
					Commands.Add(obj, cmd);
			}
		}

		/// <summary>
		/// Gets a command associated with an object.
		/// </summary>
		/// <param name="obj">The object whose command is going to be retrieved.</param>
		/// <returns>The command of the specified object.</returns>
		public static System.String GetCommand(System.Object obj)
		{
			System.String result = "";
			if (obj != null)
				result = System.Convert.ToString(Commands[obj]);
			return result;
		}



		/// <summary>
		/// Checks if the Control contains a command, if it does not it sets the default
		/// </summary>
		/// <param name="button">The control whose command will be checked</param>
		public static void CheckCommand(System.Windows.Forms.ButtonBase button)
		{
			if (button != null)
			{
				if (GetCommand(button).Equals(""))
					SetCommand(button, button.Text);
			}
		}

		/// <summary>
		/// Checks if the Control contains a command, if it does not it sets the default
		/// </summary>
		/// <param name="button">The control whose command will be checked</param>
		public static void CheckCommand(System.Windows.Forms.MenuItem menuItem)
		{
			if (menuItem != null)
			{
				if (GetCommand(menuItem).Equals(""))
					SetCommand(menuItem, menuItem.Text);
			}
		}

		/// <summary>
		/// Checks if the Control contains a command, if it does not it sets the default
		/// </summary>
		/// <param name="button">The control whose command will be checked</param>
		public static void CheckCommand(System.Windows.Forms.ComboBox comboBox)
		{
			if (comboBox != null)
			{
				if (GetCommand(comboBox).Equals(""))
					SetCommand(comboBox,"comboBoxChanged");
			}
		}

	}
	/*******************************/
	/// <summary>
	/// This method returns an Array of System.Int32 containing the size of the non client area of a control.
	/// The non client area includes elements such as scroll bars, borders, title bars, and menus.
	/// </summary>
	/// <param name="control">The control from which to retrieve the values.</param>
	/// <returns>An Array of System.Int32 containing the width of each non client area border in the following order
	/// top, left, right and bottom.</returns>
	public static System.Int32[] GetInsets(System.Windows.Forms.Control control)
	{
		System.Int32[] returnValue = new System.Int32[4];

		returnValue[0] = (control.RectangleToScreen(control.ClientRectangle).Top - control.Bounds.Top);
		returnValue[1] = (control.RectangleToScreen(control.ClientRectangle).Left  - control.Bounds.Left);
		returnValue[2] = (control.Bounds.Right - control.RectangleToScreen(control.ClientRectangle).Right);
		returnValue[3] = (control.Bounds.Bottom - control.RectangleToScreen(control.ClientRectangle).Bottom);
		return returnValue;
	}


	/*******************************/
	/// <summary>
	/// Support Methods for FileDialog class. Note that several methods receive a DirectoryInfo object, but it won't be used in all cases.
	/// </summary>
	public class FileDialogSupport
	{
		/// <summary>
		/// Creates an OpenFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog(System.IO.FileInfo path)
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = path.Directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an OpenFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog(System.IO.FileInfo path, System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = path.Directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates a OpenFileDialog open in a given path.
		/// </summary>		
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog()
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);			
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an OpenFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the OpenFileDialog</param>
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog (System.String path)
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = path;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an OpenFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of OpenFileDialog.</returns>
		public static System.Windows.Forms.OpenFileDialog CreateOpenFileDialog(System.String path, System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.OpenFileDialog temp_fileDialog = new System.Windows.Forms.OpenFileDialog();
			temp_fileDialog.InitialDirectory = path;
			return temp_fileDialog;
		}

		/// <summary>
		/// Modifies an instance of OpenFileDialog, to open a given directory.
		/// </summary>
		/// <param name="fileDialog">OpenFileDialog instance to be modified.</param>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		public static void SetOpenFileDialog(System.Windows.Forms.FileDialog fileDialog, System.String path, System.IO.DirectoryInfo directory)
		{
			fileDialog.InitialDirectory = path;
		}

		/// <summary>
		/// Modifies an instance of OpenFileDialog, to open a given directory.
		/// </summary>
		/// <param name="fileDialog">OpenFileDialog instance to be modified.</param>
		/// <param name="path">Path to be opened by the OpenFileDialog</param>
		public static void SetOpenFileDialog(System.Windows.Forms.FileDialog fileDialog, System.IO.FileInfo path)
		{
			fileDialog.InitialDirectory = path.Directory.FullName;
		}

		/// <summary>
		/// Modifies an instance of OpenFileDialog, to open a given directory.
		/// </summary>
		/// <param name="fileDialog">OpenFileDialog instance to be modified.</param>
		/// <param name="path">Path to be opened by the OpenFileDialog.</param>
		public static void SetOpenFileDialog(System.Windows.Forms.FileDialog fileDialog, System.String path)
		{
			fileDialog.InitialDirectory = path;
		}

		///
		///  Use the following static methods to create instances of SaveFileDialog.
		///  By default, JFileChooser is converted as an OpenFileDialog, the following methods
		///  are provided to create file dialogs to save files.
		///	
		
		
		/// <summary>
		/// Creates a SaveFileDialog.
		/// </summary>		
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog()
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);			
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the SaveFileDialog.</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog(System.IO.FileInfo path)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = path.Directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the SaveFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog(System.IO.FileInfo path, System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = path.Directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates a SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog(System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = directory.FullName;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the SaveFileDialog</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog (System.String path)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = path;
			return temp_fileDialog;
		}

		/// <summary>
		/// Creates an SaveFileDialog open in a given path.
		/// </summary>
		/// <param name="path">Path to be opened by the SaveFileDialog.</param>
		/// <param name="directory">Directory to get the path from.</param>
		/// <returns>A new instance of SaveFileDialog.</returns>
		public static System.Windows.Forms.SaveFileDialog CreateSaveFileDialog(System.String path, System.IO.DirectoryInfo directory)
		{
			System.Windows.Forms.SaveFileDialog temp_fileDialog = new System.Windows.Forms.SaveFileDialog();
			temp_fileDialog.InitialDirectory = path;
			return temp_fileDialog;
		}
	}
}
