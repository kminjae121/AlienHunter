namespace _01.Scipt.Player.Skill
{
    public class GunKnifeSkill : SkillCompo
    {
        private EntitySkillCompo skillCompo;

        private Player.Player _player;
        public override void GetSkill()
        {
            base.GetSkill();
            _player = _entity as Player.Player;
            skillCompo = GetComponent<EntitySkillCompo>();
        }

        protected override void Skill()
        {
            base.Skill();
        }

        public override void EventDefault()
        {
            base.EventDefault();
        }

        public override void SkillFeedback()
        {
            base.SkillFeedback();
        }
    }
}
