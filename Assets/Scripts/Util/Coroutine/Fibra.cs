using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


	public abstract class FInstruction
	{
		public abstract bool Execute (Fibra scheduler, IEnumerator task);
		public bool pause = false;
	}

	public class FiContinue : FInstruction
	{
		public override bool Execute (Fibra scheduler, IEnumerator task)
		{
			return true;
		}
	}

	public class FiWaitForSeconds : FInstruction
	{
		private float m_Second = 0;
			
		public FiWaitForSeconds(float sec)
		{
			m_Second = sec;
		}

		IEnumerator m_WaitForSecond(float delay, Fibra scheduler, IEnumerator task)
		{
			float timer = Time.time + delay;
			float leftTime = 10;

			while (0 < leftTime) 
			{
				if ( !pause )
				{
					leftTime = timer - Time.time;
				}
				else 
				{
					timer = Time.time + leftTime;
				}
				
				yield return null;
			}

			scheduler.StartFibraCoroutine(task);
		}
		
		public override bool Execute (Fibra scheduler, IEnumerator task)
		{
			scheduler.StartCoroutine(m_WaitForSecond(m_Second,scheduler, task));

			return false;
		}
	}



	public class FiWaitForCondition : FInstruction
	{
		private System.Func<bool> m_con = null;
		
		public FiWaitForCondition(System.Func<bool> con)
		{
			m_con = con;
		}
		
		IEnumerator m_WaitForCondition(System.Func<bool> con, Fibra scheduler, IEnumerator task)
		{
			System.Func<bool> _Condition = con;

			while (!_Condition()) {
				yield return null;
			}

			scheduler.StartFibraCoroutine(task);
		}
		
		public override bool Execute (Fibra scheduler, IEnumerator task)
		{
			scheduler.StartCoroutine(m_WaitForCondition(m_con,scheduler, task));
			
			return false;
		}
	}



	public class Fibra : MonoBehaviour
	{
		static List<IEnumerator> tasks = new List<IEnumerator> ();
		static Dictionary<string, IEnumerator> names = new Dictionary<string, IEnumerator> ();

		
		public void StartFibraCoroutine (string taskName, params object[] args)
		{
			var method = this.GetType ().GetMethod (taskName);
			if (args.Length == 0)
				args = null;   

			var task = method.Invoke (this, args) as IEnumerator;
			names.Add (taskName, task);
			StartFibraCoroutine (task);
		}

		public void StartFibraCoroutine (IEnumerator task)
		{
			lock(Fibra.tasks) {
				Fibra.tasks.Add (task);
			}
		}

		public void StopFibraCoroutine (string taskName)
		{
			var task = names[taskName];
			Fibra.tasks.Remove (task);
			names.Remove (taskName);
		}

		public void PauseFibraCoroutine( string taskName )
		{
			IEnumerator task = names[taskName];
			var fi = task.Current as FInstruction;
			fi.pause = true;
		}

		public void ResumeFibraCoroutine( string taskName )
		{
			IEnumerator task = names[taskName];

			if ( task == null )
				return;

			var fi = task.Current as FInstruction;
			fi.pause = false;
		}

		public int GetCountCoroutine ()
		{
			MethodInfo[] method = this.GetType().GetMethods();
			
			int z = 0;
			
			for ( int i = 0 ; i < method.Length ; i++ )
			{
				if ( method[i].ReturnType == typeof( System.Collections.IEnumerator ) )
				{
					z ++ ;
				}
			}
			
			return z;
		}


		public bool IsFibraCoroutineCount (int count )
		{
			MethodInfo[] method = this.GetType().GetMethods();
			
			int z = 0;
			
			for ( int i = 0 ; i < method.Length ; i++ )
			{
				if ( method[i].ReturnType == typeof( System.Collections.IEnumerator ) )
				{
					z ++ ;
				}
			}

			return z == count;
		}

		public bool IsFibraCoroutine ( string taskName )
		{
			MethodInfo method = this.GetType().GetMethod(taskName);

			if ( method != null )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool IsFibraCoroutine ()
		{
			MethodInfo[] method = this.GetType().GetMethods();

			bool z = false;

			for ( int i = 0 ; i < method.Length ; i++ )
			{
				if ( method[i].ReturnType == typeof( System.Collections.IEnumerator ) )
				{
					z = true;
				}
		    }

			return z;
		}

		public void Update ()
		{	
			for (int i = 0; i < Fibra.tasks.Count; i++) {

				bool removeTask = false;
				var task = tasks[i];

				if (task.MoveNext ()) 
				{
					var fi = task.Current as FInstruction;
					removeTask = !fi.Execute (this, task);
				} 
				else 
				{
					removeTask = true;
				}

				if (removeTask)
				{
					tasks.RemoveAt (i--);
				}
			}
		}
	}
