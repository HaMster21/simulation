using System;

namespace Simulation
{
	public class SimulationController
	{
		public delegate void StateUpdateHandler();
		
		#region Fields
		bool isRunning;
		int updateInterval { get; set; }

		System.Timers.Timer timer {get; set;}
		int RoundCount = 1024;
		#endregion

		public static void Main (string[] args)
		{
			SimulationController controller = new SimulationController ();
			controller.updateInterval = 16;
			controller.init ();

			controller.timer = new System.Timers.Timer(controller.updateInterval);
			controller.timer.AutoReset = true;
			controller.timer.Elapsed += new System.Timers.ElapsedEventHandler(controller.update);
		}

		public SimulationController ()
		{
			Console.WriteLine("New SimulationController created");
		}

		public void init ()
		{
			Console.WriteLine("Setting up new Simulation");
			isRunning = true;
		}

		public void update (object source, System.Timers.ElapsedEventArgs e)
		{
			Console.WriteLine ("Update in progress...");
			if (RoundCount > 0) {
				RoundCount--;

				// calling all methods that update the state of the simulation
				StateUpdateHandler();
			}
			else RequestshutDown();
		}

		public void RequestshutDown ()
		{
			isRunning = false;
			timer.Stop();
			timer.Dispose();
		}
	}
}

