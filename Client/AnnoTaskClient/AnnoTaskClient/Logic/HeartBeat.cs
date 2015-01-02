using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
	/// <summary>
	/// 작성자 : 이승철
	/// 작성일 : 20150102
	/// HeartBeat 시그널을 보내는 쓰레드,
	/// 외부에서는 시작과 끝 만 제어할 뿐 참조할 수 없음, 독립쓰레드 보장.
	/// 메인 로직과의 쓰레드 분기로 인해 clientWormHole 쪽 공유자원이 생겨 lock 처리하였음.
	/// </summary>
	class HeartBeat
	{
		private ClientWormHole clientWormHole;
		private Thread threadWorker;
		private bool running = true;

		public HeartBeat(ClientWormHole clientWormHole)
		{
			// TODO: Complete member initialization
			this.clientWormHole = clientWormHole;
		}

		/// <summary>
		/// 작성자 :이승철
		/// HeartBeat을 멈출때 사용
		/// </summary>
		public void destroyHeartBeat()
		{
			running = false;
			Thread.Sleep(600);
			threadWorker.Abort();
		}

		/// <summary>
		/// 작성자 : 이승철
		/// HeartBeat을 시작할 때 사용.
		/// </summary>
		public void run()
		{
			threadWorker = new Thread(new ThreadStart(internalRun));
			threadWorker.Name = "HeartBeat";
			threadWorker.Start();
		}

		private void internalRun()
		{
			// 이승철 추가, 20150101, 핫빗 보내기 위한 시간측정
			System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
			stopWatch.Reset();
			stopWatch.Start();

			while (running)
			{
				// HeartBeat 보내야 할 시간이 되면 보냄.
				if (stopWatch.ElapsedMilliseconds > Configure.Instance.HeartBeatDurationS)
				{
					clientWormHole.sendHeartBeat();
					stopWatch.Reset();
					stopWatch.Start();
				}

				Thread.Sleep(500);
			}
		}
	}
}
