﻿namespace TestletLibrary
{
    using System;
    using System.Collections.Generic;

    public class Testlet
    {
        public string TestletId;

        private List<Item> Items;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            throw new NotImplementedException();
            //Items private collection has 6 Operational and 4 Pretest Items. Randomize the order of these items as per the requirement (with TDD)
            //The assignment will be reviewed on the basis of – Tests written first, Correct logic, Well structured & clean readable code.
        }
    }
}
