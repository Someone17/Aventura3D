using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy{

    public class EnemyShoot : EnemyBase
    {

        public GunBase gunBase;
    
        protected override void Init(){
            base.Init();

            gunBase.StartShoot();
        }

        void Start()
        {
        
        }

    // Update is called once per frame
        void Update()
        {
        
        }
}
}
