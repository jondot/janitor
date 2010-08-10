using System;
using System.Data;
using Common.Logging;
using Quartz;

namespace Jobs
{
    /// <summary>
    /// Example Job for Janitor
    /// </summary>
    public class BirthDayJob : IJob
    {
        // * IJob interface only requires implementing 'Execute'.
        //
        // * JobExecutionContext is a data bag where we can pass values from
        //                       Job Service into this job. a way to communicate.

        private string connStr = "foo";
        public void Execute(JobExecutionContext context)
        {
            //The LogManager plugs into the Service's logger and gets it magically.
            //in test mode we have no logger, because we have no containing Service so it does nothing.
            ILog logger = LogManager.GetLogger(typeof(BirthDayJob));
            

            //Actual work below.
            try
            {
                //ds will contain rows with a single column: mail addresses.
                DataSet ds = SqlHelper.ExecuteDataset(connStr,
                                                      CommandType.Text,
                                                      "select user_email"+
                                                      " from personalcard inner join users"+
                                                      " on personalcard_user_id = user_id "+
                                                      "WHERE " + " DAY(personalcard_birth_day) = DATEPART(DAY,GETDATE()) " +
                                                      " and MONTH(personalcard_birth_day) = DATEPART(MONTH,GETDATE())");
                
                logger.Debug("found " + ds.Tables[0].Rows.Count + " emails to send birthday greetings to.");
                
                //make the result available for everyone.
                _result = ds;
            }
            catch (Exception ex)
            {
                logger.Error("could not make query: " + ex.Message);
            }
        }

        // Once the job finishes, it puts the results in this property.
        // The test framework can get the value and see if it is correct.
        private DataSet _result;
        public DataSet Result
        {
            get { return _result; }
        }
    }
}
