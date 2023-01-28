using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCoreApp
{
    public class TweetSampleStreamService
    {
        public TweetSampleStreamService()
        {

        }
        private ConcurrentDictionary<DateTime, string> concurrenSampleTweetList { get; set; } = new ConcurrentDictionary<DateTime, string>();

        public void AddStreamToList(DateTime dateTime, string id)
        {
            concurrenSampleTweetList.AddOrUpdate(dateTime, id, (dt, id) => id);
        }

        public ConcurrentDictionary<DateTime, string> GetStreamList()
        {
            return concurrenSampleTweetList;
        }
    }
}
