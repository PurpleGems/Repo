using Nez;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = Nez.Random;

namespace Joeba.Scripts.Graphics.GUI
{
    class InventoryBar : Component
    {
        public int MaxBarSlots { get; } = 30;
        private int _currentlySelected = 0;
        public int CurrentlySelected
        {
            get { return _currentlySelected; }
            set
            {
                if (value < 0) ChangeCurrentSlot(MaxBarSlots - 1);
                else if (value > MaxBarSlots - 1) ChangeCurrentSlot(0);
                else ChangeCurrentSlot(value);
            }
        }

        private UICanvas InventoryBarCanvas;
        private Table statsTable;
        Image[] inventorySlots;

        private int unselectedSize = 32;
        private int selectedSize = 80;


        public InventoryBar()
        {
            


        }

        public override void onAddedToEntity()
        {
            inventorySlots = new Image[MaxBarSlots];
            for (int i = 0; i < MaxBarSlots; i++)
            {
                inventorySlots[i] = new Image(GlobalSpritesheets.WeaponsSplit[i], Scaling.Fit, 1);
            }


            InventoryBarCanvas = entity.addComponent(new UICanvas());
            statsTable = InventoryBarCanvas.stage.addElement(new Table());
            statsTable.setFillParent(true);
            statsTable.bottom().padBottom(10);

            foreach (var VARIABLE in inventorySlots)
            {
                statsTable.add(VARIABLE).setMinWidth(unselectedSize).setMinHeight(unselectedSize).setMaxWidth(selectedSize).setMaxHeight(selectedSize);
                //VARIABLE.setSize(unselectedSize,unselectedSize);
            }

            _currentlySelected = 0;
            statsTable.getCell(inventorySlots[_currentlySelected]).size(selectedSize);

            //statsTable.debugAll();
        }


        private void ChangeCurrentSlot(int nSlot)
        {
            statsTable.getCell(inventorySlots[_currentlySelected]).size(unselectedSize);
            _currentlySelected = nSlot;
            statsTable.getCell(inventorySlots[nSlot]).size(selectedSize);
            statsTable.pack();
            //inventorySlots[_currentlySelected].setSize(unselectedSize,unselectedSize);
            //_currentlySelected = nSlot;
            //inventorySlots[nSlot].setSize(selectedSize,selectedSize);
            //statsTable.pack();
            


        }
    }
}
