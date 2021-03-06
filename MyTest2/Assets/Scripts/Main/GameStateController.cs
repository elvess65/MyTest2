﻿using mytest2.Character;
using mytest2.Character.Abilities;
using mytest2.Character.Container;
using mytest2.Items;
using UnityEngine;

namespace mytest2.Main
{
    public class GameStateController : MonoBehaviour
    {
        private GameOverController m_GameOverController;
        private DataTableAbilities m_DataTableAbilities;

        public CreatureController Player
        { get; set; }
        public DataTableAbilities DataTableAbilities
        { get; private set; }
        public DataContainerController DataContainerController
        { get; private set; }
        public ItemSpawmController ItemSpawnController
        { get; private set; }
        public AbilityColorController AbilityColorController
        { get; private set; }

        void Start()
        {
            m_GameOverController = GetComponent<GameOverController>();
            DataTableAbilities = GetComponent<DataTableAbilities>();
            DataContainerController = GetComponent<DataContainerController>();
            ItemSpawnController = GetComponent<ItemSpawmController>();
            AbilityColorController = GetComponent<AbilityColorController>();
        }

        public void GameOver()
        {
            m_GameOverController.GameOver();
        }
    }
}
