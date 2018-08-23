using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TrelloNet
{
    public class Card : ICardId, IUpdatableCard
    {
        public List<Attachment> Attachments { get; set; }

        public CardBadges Badges { get; set; }

        public List<Checklist> Checklists { get; set; }

        public bool Closed { get; set; }

        public DateTime DateLastActivity { get; set; }

        public string Desc { get; set; }

        public DateTime? Due { get; set; }

        public string Id { get; set; }

        public string IdBoard { get; set; }

        public List<string> IdLabels { get; set; }

        public string IdList { get; set; }

        public List<string> IdMembers { get; set; }

        public int IdShort { get; set; }

        public IEnumerable<Color> LabelColors { get { return Labels == null ? Enumerable.Empty<Color>() : Labels.Where(l => l.Color != null).Select(l => l.Color); } }

        public List<Label> Labels { get; set; }

        public string Name { get; set; }

        public double Pos { get; set; }

        public string ShortUrl { get; set; }

        public string Url { get; set; }

        [JsonProperty]
        private List<CheckItemState> CheckItemStates { get; set; }

        public string GetCardId()
        {
            return Id;
        }

        public override string ToString()
        {
            return Name;
        }

        [OnDeserialized]
        private void AfterDeserialization(StreamingContext context)
        {
            var checkItems = from cl in Checklists ?? Enumerable.Empty<Checklist>()
                             from ci in cl.CheckItems
                             join cis in CheckItemStates ?? Enumerable.Empty<CheckItemState>() on ci.Id equals cis.IdCheckItem
                             where cis.State == "complete"
                             select ci;

            foreach (var checkItem in checkItems)
                checkItem.Checked = true;
        }

        public class Attachment : IAttachmentId
        {
            public DateTime Date { get; set; }

            public string Id { get; set; }

            public string IdMember { get; set; }

            public string Name { get; set; }

            public string Url { get; set; }

            public string GetAttachmentId()
            {
                return Id;
            }
        }

        public class CardBadges
        {
            public int Attachments { get; set; }

            public int CheckItems { get; set; }

            public int CheckItemsChecked { get; set; }

            public int Comments { get; set; }

            public bool Description { get; set; }

            public DateTime? Due { get; set; }

            public string FogBugz { get; set; }

            public int Votes { get; set; }
        }

        // Handling of check item state :( ------------------------------- A Card has a list of
        // Checklists and a Checklist has a list of CheckItems, but a CheckItem does not include the
        // state (checked/unchecked). A Card also has a list of CheckItemState with the ID of it's
        // CheckItem and the state (which always seems to be "complete"). After we have deserialized
        // go through each CheckItemState and set Checked to true on the corresponding CheckItem.
        public class CheckItem : TrelloNet.CheckItem
        {
            public bool Checked { get; set; }
        }

        public class Checklist : Checklist<CheckItem>
        {
        }

        private class CheckItemState
        {
            public string IdCheckItem { get; set; }

            public string State { get; set; }
        }
    }
}