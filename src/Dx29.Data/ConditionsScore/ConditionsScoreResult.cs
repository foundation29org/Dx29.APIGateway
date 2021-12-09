using System;
using System.Collections.Generic;

namespace Dx29.Data
{
    public class ConditionsScoreResult {
        public IDictionary<string, DiseasesInfo> listDiseases { get; set; }
        public IDictionary<string, LogsInfo> logs { get; set; }
    }
    
    public class DiseasesInfo {
        public decimal score_genes { get; set; }
        public decimal score_symptoms { get; set; }
        public decimal score_Dx29 { get; set; }
        public string type { get; set; }
        public bool iscondition { get; set; }
        public object matches_symptoms { get; set; }
        public object matches_genes { get; set; }
        public object matches_monarch { get; set; }
    }
    
    public class LogsInfo {
        public IDictionary<string, Genes_SymptomsInfo_logs> genes { get; set; }
        public IDictionary<string, Genes_SymptomsInfo_logs> symptoms { get; set; }
        public IDictionary<string, Other_logs> other { get; set; }
        public IDictionary<string, IsCondition_logs> isCondition { get; set; }
    }
    public class Genes_SymptomsInfo_logs {
        public List<string> added { get; set; }
        public IDictionary<string, Genes_Symptoms_Discarded_logs> discarded { get; set; }
    }
    public class Genes_Symptoms_Discarded_logs {
        public List<string> previousAdded { get; set; }
    }
    public class Other_logs {
        public List<string> added { get; set; }
        public IDictionary<string, Other_Discarded_logs> discarded { get; set; }
    }
    public class Other_Discarded_logs {
        public List<string> diseases_noGenesInfo { get; set; }
    }
    public class IsCondition_logs {
        public IDictionary<string, IsCondition_Dictionary_logs> isCondition { get; set; }
    }
    public class IsCondition_Dictionary_logs {
        public IDictionary<string, List<string>> notMeetsCriteria { get; set; }
    }
    
}