using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCoreApp.Abstraction
{
    public interface ITweetsSampleProcessor
    {
        TweetResponse GetSampleTweetsCount();
    }
}
