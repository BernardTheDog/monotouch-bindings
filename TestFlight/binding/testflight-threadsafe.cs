using System;
using System.Runtime.InteropServices;

namespace MonoTouch.TestFlight
{
	public partial class TestFlight
	{
		public static void TakeOffThreadSafe(String appToken)
		{

				IntPtr sigbus = Marshal.AllocHGlobal (512);
				IntPtr sigsegv = Marshal.AllocHGlobal (512);
				IntPtr sigpipe = Marshal.AllocHGlobal (512);
				
				// Store Mono SIGSEGV and SIGBUS handlers
				sigaction (Signal.SIGBUS, IntPtr.Zero, sigbus);
				sigaction (Signal.SIGSEGV, IntPtr.Zero, sigsegv);
			  	sigaction (Signal.SIGPIPE, IntPtr.Zero, sigpipe);
				
				// Enable crash reporting libraries
				//MonoTouch.TestFlight.TestFlight.SetDeviceIdentifier(UIDevice.CurrentDevice.UniqueIdentifier);
				//MonoTouch.TestFlight.TestFlight.SetDeviceIdentifier(MonoTouch.AdSupport.ASIdentifierManager.SharedManager.AdvertisingIdentifier.ToString());
				TestFlight.TakeOff(appToken);
				
				// Restore Mono SIGSEGV and SIGBUS handlers            
				sigaction (Signal.SIGBUS, sigbus, IntPtr.Zero);
				sigaction (Signal.SIGSEGV, sigsegv, IntPtr.Zero);
				sigaction (Signal.SIGPIPE, sigpipe, IntPtr.Zero);
		 		
				Marshal.FreeHGlobal (sigbus);
				Marshal.FreeHGlobal (sigsegv);
				Marshal.FreeHGlobal (sigpipe);
		}

		[DllImport ("libc")]
		private static extern int sigaction (Signal sig, IntPtr act, IntPtr oact);
		enum Signal {
			SIGBUS = 10,
			SIGSEGV = 11,
			SIGPIPE = 13
		}
	}
}

