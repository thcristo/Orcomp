﻿namespace NPerf.Fixture.ISorterInt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using NPerf.Framework;

    using Orc.Algorithms.Interfaces;

    [PerfTester(typeof(ISorter<int>), 10, Description = "Sort Algorithm benchmark", FeatureDescription = "Collection size")]
    public class SmallNumberOfElementsTester
    {
        private List<int> list;

        public int CollectionCount(int testIndex)
        {
            int n = 0;

            if (testIndex < 0)
            {
                n = 10;
            }
            else
            {
                n = (testIndex + 1) * 100;
            }

            return n;
        }

        [PerfRunDescriptor]
        public double RunDescription(int testIndex)
        {
            return this.CollectionCount(testIndex);
        }

        [PerfSetUp]
        public void SetUp(int testIndex, ISorter<int> sorter)
        {
            Random rnd = new Random();
            this.list = new List<int>();

            for (int i = 0; i < this.CollectionCount(testIndex); ++i)
            {
                this.list.Add(rnd.Next());
            }
        }

        [PerfTest]
        public void Sort(ISorter<int> sorter)
        {
            sorter.Sort(this.list);
        }

        [PerfTearDown]
        public void TearDown(ISorter<int> sorter)
        {
            // checking up
            for (int i = 0; i < this.list.Count - 1; ++i)
            {
                if (this.list[i] > this.list[i + 1])
                {
                    throw new Exception("list not sorted");
                }
            }
        }
    }
}