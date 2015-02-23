using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace UnityTest
{

	[TestFixture]
	[Category("Gadrian Tests")]
	public class MoodTest
	{
		
		[Test]	
		public void ConstMoodsVsFeeling ()
		{
			// Intensity is consistent?

			Assert.That ( Mood.HAPPY.getIntensity (), Is.EqualTo(1f).Within(1e-6));
			Assert.That ( Mood.ANGRY.getIntensity (), Is.EqualTo(1f).Within(1e-6));
			Assert.That ( Mood.SCARED.getIntensity (), Is.EqualTo(1f).Within(1e-6));

			//requires more tolerance due to proximity to indiferent
			Assert.That ( Mood.SAD.getIntensity (), Is.EqualTo(1f).Within(1e-4));

			// Feel is consistent?
			Assert.AreEqual (Mood.Feeling.HAPPY, Mood.HAPPY.getFeel ());
			Assert.AreEqual (Mood.Feeling.ANGRY, Mood.ANGRY.getFeel ());
			Assert.AreEqual (Mood.Feeling.SAD, Mood.SAD.getFeel ());
			Assert.AreEqual (Mood.Feeling.INDIFERENT, Mood.INDIFERENT.getFeel ());
			Assert.AreEqual (Mood.Feeling.SCARED, Mood.SCARED.getFeel ());

			Assert.AreNotEqual (Mood.INDIFERENT, Mood.SAD);
			Assert.AreNotEqual (Mood.INDIFERENT, Mood.HAPPY);
			Assert.AreNotEqual (Mood.INDIFERENT, Mood.ANGRY);
			Assert.AreNotEqual (Mood.INDIFERENT, Mood.SCARED);
		}

		[Test]
		public void FreeVectorVsFeeling ()
		{
			Mood m = new Mood (Vector2.right* .8f);
			Assert.AreEqual (Mood.Feeling.HAPPY, m.getFeel ());

			m = new Mood (Vector2.up);
			Assert.AreEqual (Mood.Feeling.PERPLEX, m.getFeel ());

			m = new Mood (-Vector2.up* .7f);
			Assert.AreEqual (Mood.Feeling.ANGRY, m.getFeel ());

			m = new Mood (-Vector2.right* .6f);
			Assert.AreEqual (Mood.Feeling.SCARED, m.getFeel ());

			m = new Mood (Vector2.up * .2f);
			Assert.AreEqual (Mood.Feeling.SAD, m.getFeel ());

			m = new Mood (Vector2.up * .5f);
			Assert.AreEqual (Mood.Feeling.PERPLEX, m.getFeel ());
		}

		[Test]
		public void MoodLerp ()
		{
			Assert.AreEqual (Mood.Feeling.HAPPY, Mood.Lerp (Mood.HAPPY, Mood.ANGRY, .0f).getFeel ());
			Assert.AreEqual (Mood.Feeling.PERPLEX, Mood.Lerp (Mood.HAPPY, Mood.ANGRY, .5f).getFeel ());
			Assert.AreEqual (Mood.Feeling.ANGRY, Mood.Lerp (Mood.HAPPY, Mood.ANGRY, 1.0f).getFeel ());
			
			Assert.AreEqual (Mood.Feeling.HAPPY, Mood.Lerp (Mood.HAPPY, Mood.SCARED, .0f).getFeel ());
			Assert.AreEqual (Mood.Feeling.PERPLEX, Mood.Lerp (Mood.HAPPY, Mood.SCARED, .5f).getFeel ());
			Assert.AreEqual (Mood.Feeling.SCARED, Mood.Lerp (Mood.HAPPY, Mood.SCARED, 1.0f).getFeel ());
			
			Assert.AreEqual (Mood.Feeling.HAPPY, Mood.Lerp (Mood.HAPPY, Mood.SAD, .0f).getFeel ());
			Assert.AreEqual (Mood.Feeling.PERPLEX, Mood.Lerp (Mood.HAPPY, Mood.SAD, .5f).getFeel ());
			Assert.AreEqual (Mood.Feeling.SAD, Mood.Lerp (Mood.HAPPY, Mood.SAD, 1.0f).getFeel ());

			//Probably the worst case
			Assert.AreEqual (Mood.Feeling.SAD, Mood.Lerp (Mood.SAD, Mood.ANGRY, .0f).getFeel ());
			Assert.AreEqual (Mood.Feeling.PERPLEX, Mood.Lerp (Mood.SAD, Mood.ANGRY, .5f).getFeel ());
			Assert.AreEqual (Mood.Feeling.ANGRY, Mood.Lerp (Mood.SAD, Mood.ANGRY, 1.0f).getFeel ());
		}

		//[Test] public void working (){}

		[Test]
		public void MoodAccumulation ()
		{
			Mood accum;
			accum = Mood.INDIFERENT;
			accum += Mood.HAPPY;
			Assert.AreEqual(Mood.Feeling.HAPPY, accum.getFeel());

			accum = Mood.HAPPY;
			accum += Mood.HAPPY;
			Assert.AreEqual(Mood.Feeling.HAPPY, accum.getFeel());
			
			accum = Mood.ANGRY;
			accum += Mood.ANGRY;
			Assert.AreEqual(Mood.Feeling.ANGRY, accum.getFeel());
			
			accum = Mood.SCARED;
			accum += Mood.SCARED;
			Assert.AreEqual(Mood.Feeling.SCARED, accum.getFeel());

			accum = Mood.HAPPY;
			accum += Mood.HAPPY;
			accum += Mood.ANGRY;
			Assert.AreEqual(Mood.Feeling.HAPPY, accum.getFeel());

			accum = Mood.ANGRY;
			accum += Mood.ANGRY;
			accum += Mood.HAPPY;
			Assert.AreEqual(Mood.Feeling.ANGRY, accum.getFeel());
			
			accum = Mood.SCARED;
			accum += Mood.SCARED;
			accum += Mood.ANGRY;
			Assert.AreEqual(Mood.Feeling.SCARED, accum.getFeel());

			//SAD + AnyMood = SAD
			accum = Mood.HAPPY;
			accum += Mood.SAD;
			Assert.AreEqual(Mood.Feeling.SAD, accum.getFeel());
			
			accum = Mood.SAD;
			accum += Mood.ANGRY;
			Assert.AreEqual(Mood.Feeling.SAD, accum.getFeel());
			
			accum = Mood.SCARED;
			accum += Mood.SAD;
			Assert.AreEqual(Mood.Feeling.SAD, accum.getFeel());
		}
	}

}