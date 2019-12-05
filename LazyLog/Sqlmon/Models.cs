using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazylog
{

    class sp_lock2_TypeA
    {
        public string ip { get; set; }
        public string port { get; set; }
        public string probe_time { get; set; }
        public string hh_mm_ss { get; set; }
        public long wait_sec { get; set; }
        public string locktree { get; set; }
        public int spid { get; set; }
        public int kpid { get; set; }
        public int blocked { get; set; }
        public string waittype { get; set; }
        public long waittime { get; set; }
        public string lastwaittype { get; set; }
        public string waitresource { get; set; }
        public int dbid { get; set; }
        public int uid { get; set; }
        public long cpu { get; set; }
        public long physical_io { get; set; }
        public long memusage { get; set; }
        public DateTime login_time { get; set; }
        public DateTime last_batch { get; set; }
        public int ecid { get; set; }
        public int open_tran { get; set; }
        public string status { get; set; }
        //public string sid { get; set; }
        public string sid { get; set; }
        public string hostname { get; set; }
        public string program_name { get; set; }
        public string hostprocess { get; set; }
        public string cmd { get; set; }
        public string nt_domain { get; set; }
        public string nt_username { get; set; }
        public string net_address { get; set; }
        public string net_library { get; set; }
        public string loginame { get; set; }
        //public string context_info { get; set; }
        public string context_info { get; set; }
        //public string sql_handle { get; set; }
        public string sql_handle { get; set; }
        public int stmt_start { get; set; }
        public int stmt_end { get; set; }
        public int request_id { get; set; }
        public int objectid { get; set; }
        public int number { get; set; }
        //public string encrypted { get; set; }
        public string encrypted { get; set; }

        public string text { get; set; }
    }


    class sp_lock2_data
    {
        //public string MachineName { get; set; }
        //public string FullInstanceName { get; set; }
        //public string MachinePrivateIp { get; set; }
        public DateTime probe_time { get; set; }
        public string hh_mm_ss { get; set; }
        public long wait_sec { get; set; }
        public string locktree { get; set; }
        public int spid { get; set; }
        public int kpid { get; set; }
        public int blocked { get; set; }
        public byte[] waittype { get; set; }
        public long waittime { get; set; }
        public string lastwaittype { get; set; }
        public string waitresource { get; set; }
        public int dbid { get; set; }
        public int uid { get; set; }
        public long cpu { get; set; }
        public long physical_io { get; set; }
        public long memusage { get; set; }
        public DateTime login_time { get; set; }
        public DateTime last_batch { get; set; }
        public int ecid { get; set; }
        public int open_tran { get; set; }
        public string status { get; set; }
        //public string sid { get; set; }
        public byte[] sid { get; set; }
        public string hostname { get; set; }
        public string program_name { get; set; }
        public string hostprocess { get; set; }
        public string cmd { get; set; }
        public string nt_domain { get; set; }
        public string nt_username { get; set; }
        public string net_address { get; set; }
        public string net_library { get; set; }
        public string loginame { get; set; }
        //public string context_info { get; set; }
        public byte[] context_info { get; set; }
        //public string sql_handle { get; set; }
        public byte[] sql_handle { get; set; }
        public int stmt_start { get; set; }
        public int stmt_end { get; set; }
        public int request_id { get; set; }
        public int objectid { get; set; }
        public int number { get; set; }
        //public string encrypted { get; set; }
        public bool encrypted { get; set; }
        
        public string text { get; set; }
    }


    class dm_os_workers_data
    {

        public int idx { get; set; }
        //public string MachineName { get; set; }
        //public string FullInstanceName { get; set; }
        //public string MachinePrivateIp { get; set; }
        public DateTime probe_time { get; set; }
        public int session_limit { get; set; }

        public int current_session_cnt { get; set; }
        public int max_worker_thread { get; set; }
        public int current_worker_cnt { get; set; }
        public int scheduler_id { get; set; }
        public long quantum_used { get; set; }

        public bool is_preemptive { get; set; }
        public long context_switch_count { get; set; }
        public string state { get; set; }
        public string last_wait_type { get; set; }
        public int processor_group { get; set; }

        public int tasks_processed_count { get; set; }
        public byte[] task_address { get; set; }
        public int session_id { get; set; }
        public string original_login_name { get; set; }
        public string host_name { get; set; }

        public string program_name { get; set; }
        public string command { get; set; }
        public long cpu_time { get; set; }
        public long total_elapsed_time { get; set; }
        public long reads { get; set; }

        public long writes { get; set; }
        public long logical_reads { get; set; }
        public byte[] query_hash { get; set; }
        public byte[] sql_handle { get; set; }
        public int statement_start_offset { get; set; }

        public int statement_end_offset { get; set; }
        public int database_id { get; set; }
        public int blocking_session_id { get; set; }
        public int open_transaction_count { get; set; }
        public double percent_complete { get; set; }

        public int transaction_isolation_level { get; set; }
        public byte[] query_plan_hash { get; set; }
        public byte[] plan_handle { get; set; }

        public string query_text { get; set; }
    }

    class dm_os_workers_data_TypeA
    {

        public string ip { get; set; }
        public string port { get; set; }
        //public int idx { get; set; }
        //public string MachineName { get; set; }
        //public string FullInstanceName { get; set; }
        //public string MachinePrivateIp { get; set; }
        public string probe_time { get; set; }
        public int session_limit { get; set; }

        public int current_session_cnt { get; set; }
        public int max_worker_thread { get; set; }
        public int current_worker_cnt { get; set; }
        public int scheduler_id { get; set; }
        public long quantum_used { get; set; }

        public string is_preemptive { get; set; }
        public long context_switch_count { get; set; }
        public string state { get; set; }
        public string last_wait_type { get; set; }
        public short processor_group { get; set; }

        public int tasks_processed_count { get; set; }
        public string task_address { get; set; }
        public int session_id { get; set; }
        public string original_login_name { get; set; }
        public string host_name { get; set; }

        public string program_name { get; set; }
        public string command { get; set; }
        public long cpu_time { get; set; }
        public long total_elapsed_time { get; set; }
        public long reads { get; set; }

        public long writes { get; set; }
        public long logical_reads { get; set; }
        public string query_hash { get; set; }
        public string sql_handle { get; set; }
        public int statement_start_offset { get; set; }

        public int statement_end_offset { get; set; }
        public short database_id { get; set; }
        public int blocking_session_id { get; set; }
        public int open_transaction_count { get; set; }
        public Single percent_complete { get; set; }

        public short transaction_isolation_level { get; set; }
        public string query_plan_hash { get; set; }
        public string plan_handle { get; set; }
        public string query_text { get; set; }
    }

    class sp_readerrorlog_data
    {
        public DateTime LogDate { get; set; }
        public string ProcessInfo { get; set; }
        public string Text { get; set; }
    }

    class sp_readagentlog_data
    {
        public DateTime LogDate { get; set; }
        public string ErrorLevel { get; set; }
        public string Text { get; set; }
    }

    class sp_readerrorlog_TypeB_old
    {
        public string MachineName { get; set; }
        public string FullInstanceName { get; set; }
        public string MachinePrivateIp { get; set; }
        public string probe_time { get; set; }
        public DateTime LogDate { get; set; }
        public string ProcessInfo { get; set; }
        public string Text { get; set; }
        public string LogFileType { get; set; }
    }

    class sp_readerrorlog_TypeB
    {
        public string key { get; set; }
        public string type { get; set; }
        public string timestamp { get; set; }
        public string subject { get; set; }
        public string hostname { get; set; }
        public string ip { get; set; }
        public long probe_time { get; set; }
        public long LogDate { get; set; }
        public string ProcessInfo { get; set; }
        public string LogFileType { get; set; }
        public string message { get; set; }
    }

    class sp_readerrorlog_TypeA
    {
        public string ip { get; set; }
        public string port { get; set; }
        public string probe_time { get; set; }
        public DateTime LogDate { get; set; }
        public string ProcessInfo { get; set; }
        public string Text { get; set; }
    }

    class sp_readagentlog_TypeA
    {
        public string ip { get; set; }
        public string port { get; set; }
        public string probe_time { get; set; }
        public DateTime LogDate { get; set; }
        public string ErrorLevel { get; set; }
        public string Text { get; set; }
    }

    class dm_exec_query_stats_data
    {
        public long execution_count          { get; set; }
        public long total_worker_time        { get; set; }
        public long total_logical_reads      { get; set; }
        public long total_physical_reads     { get; set; }
        public long total_logical_writes     { get; set; }
        public long total_elapsed_time       { get; set; }
        public long total_grant_kb           { get; set; }
        public byte[] sql_handle             { get; set; }
        public int statement_start_offset    { get; set; }
        public int statement_end_offset      {get; set; }
        public byte[] plan_handle            { get; set; }
        public byte[] query_hash             { get; set; }
        public byte[] query_plan_hash        { get; set; }
    }

    class dm_exec_query_stats_typeA
    {
        public string ip { get; set; }
        public string port { get; set; }
        public string probe_time { get; set; }
        public long execution_count { get; set; }
        public long total_worker_time { get; set; }
        public long total_logical_reads { get; set; }
        public long total_physical_reads { get; set; }
        public long total_logical_writes { get; set; }
        public long total_elapsed_time { get; set; }
        public long total_grant_kb { get; set; }
        public string sql_handle { get; set; }
        public int statement_start_offset { get; set; }
        public int statement_end_offset { get; set; }
        public string plan_handle { get; set; }
        public string query_hash { get; set; }
        public string query_plan_hash { get; set; }
    }

    class dm_exec_query_stats_statement_TypeA
    {
        public string ip { get; set; }
        public string port { get; set; }
        public string probe_time { get; set; }
        public string query_hash { get; set; }
        public string query_statement { get; set; }
    }
}
