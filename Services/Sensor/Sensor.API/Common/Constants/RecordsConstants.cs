namespace Sensor.API.Common.Constants
{
    /// <summary>
    /// Record common constants.
    /// </summary>
    public class RecordsConstants
    {
        /// <summary>
        /// Record already exists.
        /// </summary>
        public static string RECORD_ALREADY_EXIST { get; set; } = "Record already exists!";

        /// <summary>
        /// Record not found.
        /// </summary>
        public static string RECORD_NOT_FOUND { get; set; } = "Record not found!";

        /// <summary>
        /// Record not found.
        /// </summary>
        public static string GET_FOUND_RECORD { get; set; } = "Record found.";

        /// <summary>
        /// Record has an empty value.
        /// </summary>
        public static string EMPTY_RECORD_VALUE { get; set; } = "Record has an empty value!";

        /// <summary>
        /// Record recieved.
        /// </summary>
        public static string GET_RECORDS { get; set; } = "Records recieved.";

        /// <summary>
        /// Record registration conflict.
        /// </summary>
        public static string ADD_RECORD_CONFLICT { get; set; } = "New record registration conflict!";

        /// <summary>
        /// New record was successfully registered.
        /// </summary>
        public static string ADD_RECORD_SUCCESS { get; set; } = "New record was successfullty registered!";

        /// <summary>
        /// Record update conflict.
        /// </summary>
        public static string UPDATE_RECORD_CONFLICT { get; set; } = "Record update conflict!";

        /// <summary>
        /// Record update success.
        /// </summary>
        public static string UPDATE_RECORD_SUCCESS { get; set; } = "Record update success!";

        /// <summary>
        /// Record deletion success.
        /// </summary>
        public static string DELETE_RECORD_SUCCESS { get; set; } = "Record deletion success!";
    }
}