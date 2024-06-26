﻿using ApiSharedClasses.QueryParameters.Abstract;

namespace ApiSharedClasses.QueryParameters
{
	public class NoteParameters : AGetParameters
	{
		#region Properties

		public DateTimeOffset MinDateOfCreation { get; set; } = new DateTimeOffset(new DateTime(2020, 1, 1));
		public DateTimeOffset MaxDateOfCreation { get; set; } = DateTimeOffset.Now.AddHours(24);   //DateTimeOffset.MaxValue;
		public bool? HasPicture { get; set; }

		#endregion
		#region Methods
		//These are methods, because Controllers take these classes as "FromQuery" parameters and check values are not needed there
		public bool ValidTimeRange() => MaxDateOfCreation > MinDateOfCreation;

		#endregion
	}
}
