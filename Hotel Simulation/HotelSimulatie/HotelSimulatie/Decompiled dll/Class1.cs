// Decompiled with JetBrains decompiler
// Type: HotelEvents.HotelEventManager
// Assembly: HotelEvents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEAFD57A-9AD5-4533-8570-97C89FB7AF75
// Assembly location: C:\Users\Stefan\Source\Repos\HotelSimulatie\HotelSimulatie\HotelSimulatie\Content\HotelEvents.dll

using System;
using System.Collections.Generic;
using System.Threading;

namespace HotelEvents
{
    public static class HotelEventManager
    {
        private static List<HotelEventListener> A = new List<HotelEventListener>();
        private static List<HotelEvent> A = new List<HotelEvent>();
        private static bool A = false;
        private static bool a = false;
        private static bool B = false;
        private static Random A = new Random();
        private static DateTime A;

        public static bool Running
        {
            get
            {
                return !HotelEventManager.A;
            }
        }

        public static float HTE_Factor { get; set; }

        static HotelEventManager()
        {
            HotelEventManager.HTE_Factor = 0.5f;
            HotelEventManager.A.Add(new HotelEvent()
            {
                EventType = HotelEventType.CHECK_IN,
                Message = .A(),
                Time = 2000,
                Data = new Dictionary<string, string>()
        {
          {
            .a(),
            .B()
          }
        }
            });
            HotelEventManager.A.Add(new HotelEvent()
            {
                EventType = HotelEventType.CHECK_IN,
                Message = .b(),
                Time = 5000,
                Data = new Dictionary<string, string>()
        {
          {
            .C(),
            .c()
          }
        }
            });
            HotelEventManager.A.Add(new HotelEvent()
            {
                EventType = HotelEventType.CHECK_IN,
                Message = .D(),
                Time = 6000,
                Data = new Dictionary<string, string>()
        {
          {
            .d(),
            .E()
          }
        }
            });
            HotelEventManager.A.Add(new HotelEvent()
            {
                Message = .e(),
                Time = 7000
            });
            HotelEventManager.A.Add(new HotelEvent()
            {
                Message = .F(),
                Time = 7500
            });
            HotelEventManager.A.Add(new HotelEvent()
            {
                Message = .f(),
                Time = 8000
            });
        }

        public static void Register(HotelEventListener listener)
        {
            HotelEventManager.A.Add(listener);
        }

        public static bool Deregister(HotelEventListener listener)
        {
            bool flag = false;
            if (HotelEventManager.A.Contains(listener))
            {
                HotelEventManager.A.Remove(listener);
                flag = true;
            }
            return flag;
        }

        public static void Start()
        {
            new Thread((ThreadStart)(() =>
            {
                HotelEventManager.A = DateTime.Now;
                while (!HotelEventManager.A)
                    HotelEventManager.a();
                HotelEventManager.A = false;
            })).Start();
        }

        public static void Pauze()
        {
            HotelEventManager.a = !HotelEventManager.a;
        }

        public static void Stop()
        {
            HotelEventManager.A = true;
        }

        private static void A()
        {
            if (HotelEventManager.A.Next(100) >= 5)
                return;
            int num = HotelEventManager.A.Next(1, 6);
            List<HotelEvent> a = HotelEventManager.A;
            int index = 0;
            HotelEvent hotelEvent = new HotelEvent();
            hotelEvent.EventType = HotelEventType.CHECK_IN;
            string str = string.Format(.G(), (object)num);
            hotelEvent.Message = str;
            int time = HotelEventManager.A[0].Time;
            hotelEvent.Time = time;
            hotelEvent.Data = new Dictionary<string, string>()
      {
        {
          .g(),
          string.Format(.G(), (object) num)
        }
      };
            a.Insert(index, hotelEvent);
            HotelEventManager.B = true;
        }

        private static void a()
        {
            if (HotelEventManager.a)
                return;
            if (HotelEventManager.A.Count == 0)
            {
                HotelEventManager.Stop();
            }
            else
            {
                TimeSpan timeSpan = DateTime.Now - HotelEventManager.A;
                if (timeSpan.Seconds % 2 == 0 && !HotelEventManager.B)
                    HotelEventManager.A();
                if ((uint)(timeSpan.Seconds % 2) > 0U)
                    HotelEventManager.B = false;
                if ((double)HotelEventManager.A[0].Time / (double)HotelEventManager.HTE_Factor > timeSpan.TotalMilliseconds)
                    return;
                foreach (HotelEventListener hotelEventListener in HotelEventManager.A)
                    hotelEventListener.Notify(HotelEventManager.A[0]);
                HotelEventManager.A.RemoveAt(0);
            }
        }
    }
}
// Decompiled with JetBrains decompiler
// Type: HotelEvents.HotelEventType
// Assembly: HotelEvents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEAFD57A-9AD5-4533-8570-97C89FB7AF75
// Assembly location: C:\Users\Stefan\Source\Repos\HotelSimulatie\HotelSimulatie\HotelSimulatie\Content\HotelEvents.dll

namespace HotelEvents
{
    public enum HotelEventType
    {
        CHECK_IN,
        CHECK_OUT,
        CLEANING_EMERGENCY,
        EVACUATE,
        GODZILLA,
        NEED_FOOD,
        GOTO_CINEMA,
        GOTO_FITNESS,
        STAR_CINEMA,
    }
}
// Decompiled with JetBrains decompiler
// Type: HotelEvents.HotelEventListener
// Assembly: HotelEvents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEAFD57A-9AD5-4533-8570-97C89FB7AF75
// Assembly location: C:\Users\Stefan\Source\Repos\HotelSimulatie\HotelSimulatie\HotelSimulatie\Content\HotelEvents.dll

namespace HotelEvents
{
    public interface HotelEventListener
    {
        void Notify(HotelEvent evt);
    }
}

// Decompiled with JetBrains decompiler
// Type: HotelEvents.HotelEvent
// Assembly: HotelEvents, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEAFD57A-9AD5-4533-8570-97C89FB7AF75
// Assembly location: C:\Users\Stefan\Source\Repos\HotelSimulatie\HotelSimulatie\HotelSimulatie\Content\HotelEvents.dll

using System.Collections.Generic;

namespace HotelEvents
{
    public class HotelEvent
    {
        public HotelEventType EventType { get; set; }

        public string Message { get; set; }

        public int Time { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }
}
// Decompiled with JetBrains decompiler
// Type: Microsoft.Xna.Framework.Audio.NoAudioHardwareException
// Assembly: MonoGame.Framework, Version=3.6.0.1625, Culture=neutral, PublicKeyToken=null
// MVID: 031203BD-A1EB-481C-833F-235A84DE74CE
// Assembly location: C:\Users\Stefan\Source\Repos\HotelSimulatie\HotelSimulatie\HotelSimulatie\bin\Windows\x86\Debug\MonoGame.Framework.dll

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework.Audio
{
    /// <summary>
    /// The exception thrown when no audio hardware is present, or driver issues are detected.
    /// </summary>
    [DataContract]
    public sealed class NoAudioHardwareException : ExternalException
    {
        /// <param name="msg">A message describing the error.</param>
        public NoAudioHardwareException(string msg)
          : base(msg)
        {
        }

        /// <param name="msg">A message describing the error.</param>
        /// <param name="innerException">The exception that is the underlying cause of the current exception. If not null, the current exception is raised in a try/catch block that handled the innerException.</param>
        public NoAudioHardwareException(string msg, Exception innerException)
          : base(msg, innerException)
        {
        }
    }
}
// Decompiled with JetBrains decompiler
// Type: Microsoft.Xna.Framework.Audio.AudioChannels
// Assembly: MonoGame.Framework, Version=3.6.0.1625, Culture=neutral, PublicKeyToken=null
// MVID: 031203BD-A1EB-481C-833F-235A84DE74CE
// Assembly location: C:\Users\Stefan\Source\Repos\HotelSimulatie\HotelSimulatie\HotelSimulatie\bin\Windows\x86\Debug\MonoGame.Framework.dll

namespace Microsoft.Xna.Framework.Audio
{
    /// <summary>
    /// Represents how many channels are used in the audio data.
    /// </summary>
    public enum AudioChannels
    {
        Mono = 1,
        Stereo = 2,
    }
}
