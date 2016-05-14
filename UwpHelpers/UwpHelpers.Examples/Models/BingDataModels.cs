using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UwpHelpers.Examples.Models
{
    [DataContract]
    public class BingImagesRoot
    {
        [DataMember]
        public List<BingImage> images { get; set; }
        [DataMember]
        public Tooltips tooltips { get; set; }
    }

    [DataContract]
    public class Tooltips
    {
        [DataMember]
        public string loading { get; set; }
        [DataMember]
        public string previous { get; set; }
        [DataMember]
        public string next { get; set; }
        [DataMember]
        public string walle { get; set; }
        [DataMember]
        public string walls { get; set; }
    }

    [DataContract]
    public class BingImage
    {
        [DataMember]
        public string startdate { get; set; }
        [DataMember]
        public string fullstartdate { get; set; }
        [DataMember]
        public string enddate { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string urlbase { get; set; }
        [DataMember]
        public string copyright { get; set; }
        [DataMember]
        public string copyrightlink { get; set; }
        [DataMember]
        public bool wp { get; set; }
        [DataMember]
        public string hsh { get; set; }
        [DataMember]
        public int drk { get; set; }
        [DataMember]
        public int top { get; set; }
        [DataMember]
        public int bot { get; set; }
        [DataMember]
        public List<ImageHints> hs { get; set; }
        [DataMember]
        public object[] msg { get; set; }
    }

    [DataContract]
    public class ImageHints
    {
        [DataMember]
        public string desc { get; set; }
        [DataMember]
        public string link { get; set; }
        [DataMember]
        public string query { get; set; }
        [DataMember]
        public int locx { get; set; }
        [DataMember]
        public int locy { get; set; }
    }
}
