using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class SpeechPlay
    {
        public static void SpeakContent(string content)
        {

            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SpeakAsync(content);
            Thread.Sleep(400 * content.Count());
        }
    }
}
