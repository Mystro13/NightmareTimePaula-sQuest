public enum MovementCategoryOptions
{
   MoveTowardsTarget,
   RotateTowardsTarget,
   SeekTarget,
   //When enemy's health is below a certain value (25%), the enemy will run away from the player
   //the enemey will go in opposite direction from player position.
   FleeingFromAPlayer,
   //Enemy attack should have a duration ...
   AttackingAPlayer,
   PursueingAPlayer,
   FollowTarget,
   PatrollingAnAreaPath,
   FindingCover,
   WanderRandomly,
   SearchingForAPlayer,
   WithinDistance,
   CanSeeObject,
   CanHearTarget,
   FlockTogether,
   FollowALeader,
   TakePlaceInQueue,
   //Ennemy needs to stand in place while the idle animation is playing
   StayIdle,
   //When Player dies
   RevivingInGraveyard,
   CouncilInsultDialague,
   //Guide needs to be there to wake the player and help the player unsterstand new gear and obstaclses along the way
   GuideDialague,
   LeadPlayerToEntrence,
   TeaachPlayerAboutWeapon,
   TeachPlayerAboutTool,
   TeachPlayerAboutObstacle,
   WarnThePlayer

}
