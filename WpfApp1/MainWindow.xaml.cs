using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1 {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow:Window {
		static bool is64BitProcess = (IntPtr.Size == 8);
		static bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();
		[DllImport("kernel32.dll",SetLastError = true,CallingConvention = CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process(
				[In] IntPtr hProcess,
				[Out] out bool wow64Process
		);
		public static bool InternalCheckIsWow64() {
			if((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
					Environment.OSVersion.Version.Major >= 6) {
				using(Process p = Process.GetCurrentProcess()) {
					bool retVal;
					if(!IsWow64Process(p.Handle,out retVal)) {
						return false;
					}
					return retVal;
				}
			} else {
				return false;
			}
		}
		public MainWindow() {
			InitializeComponent();
			this.Platform.Text=InternalCheckIsWow64() ? "x86" : "x64";
		}
	}
}
