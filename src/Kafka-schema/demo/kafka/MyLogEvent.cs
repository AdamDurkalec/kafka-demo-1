// ------------------------------------------------------------------------------
// <auto-generated>
//    Generated by avrogen, version 1.11.0.0
//    Changes to this file may cause incorrect behavior and will be lost if code
//    is regenerated
// </auto-generated>
// ------------------------------------------------------------------------------
namespace demo.kafka
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Avro;
	using Avro.Specific;
	
	public partial class MyLogEvent : ISpecificRecord
	{
		public static Schema _SCHEMA = Avro.Schema.Parse(@"{""type"":""record"",""name"":""MyLogEvent"",""namespace"":""demo.kafka"",""fields"":[{""name"":""LogMessage"",""type"":""string""},{""name"":""OccurenceTimeStamp"",""doc"":""Occurence UTC time stamp in miliseconds"",""type"":""long""},{""name"":""LogLevel"",""type"":{""type"":""enum"",""name"":""LogLevel"",""namespace"":""demo.kafka"",""symbols"":[""Info"",""Warning"",""Debug"",""Error""]}}]}");
		private string _LogMessage;
		/// <summary>
		/// Occurence UTC time stamp in miliseconds
		/// </summary>
		private long _OccurenceTimeStamp;
		private demo.kafka.LogLevel _LogLevel;
		public virtual Schema Schema
		{
			get
			{
				return MyLogEvent._SCHEMA;
			}
		}
		public string LogMessage
		{
			get
			{
				return this._LogMessage;
			}
			set
			{
				this._LogMessage = value;
			}
		}
		/// <summary>
		/// Occurence UTC time stamp in miliseconds
		/// </summary>
		public long OccurenceTimeStamp
		{
			get
			{
				return this._OccurenceTimeStamp;
			}
			set
			{
				this._OccurenceTimeStamp = value;
			}
		}
		public demo.kafka.LogLevel LogLevel
		{
			get
			{
				return this._LogLevel;
			}
			set
			{
				this._LogLevel = value;
			}
		}
		public virtual object Get(int fieldPos)
		{
			switch (fieldPos)
			{
			case 0: return this.LogMessage;
			case 1: return this.OccurenceTimeStamp;
			case 2: return this.LogLevel;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Get()");
			};
		}
		public virtual void Put(int fieldPos, object fieldValue)
		{
			switch (fieldPos)
			{
			case 0: this.LogMessage = (System.String)fieldValue; break;
			case 1: this.OccurenceTimeStamp = (System.Int64)fieldValue; break;
			case 2: this.LogLevel = (demo.kafka.LogLevel)fieldValue; break;
			default: throw new AvroRuntimeException("Bad index " + fieldPos + " in Put()");
			};
		}
	}
}
