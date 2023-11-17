using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramManageMenu;

namespace ManageMenu
{
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	public class clsManageMenu : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			frmSetupMenu frmSetupMenu = new frmSetupMenu(commandData);
			frmSetupMenu.ShowDialog();
			frmSetupMenu = null;
			return 0;
		}
	}
}
