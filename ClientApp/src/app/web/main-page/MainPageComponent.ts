import { Component } from '@angular/core';
import { Additem } from 'src/app/interfaces/additem';
import { AlertOptions } from 'src/app/interfaces/aleart-options';
import { Food } from 'src/app/interfaces/food';
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';


@Component({
    selector: 'app-main-page',
    templateUrl: './main-page.component.html',
    styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent {
    Food: Food[] = [];
    additem: Additem = { ItemId: '', Quantity: 0 };
    options: AlertOptions;
    constructor(private user: UserService) {
        this.LoadFood();
    }


    LoadFood() {
        this.user.GetFoods().subscribe((foods) => {
            this.Food = foods;
        });
    }


    add_tobasket(id: string) {
        this.additem.ItemId = id;
        this.additem.Quantity = 3;
        this.user.Add_Item_To_Basket(this.additem).subscribe((response) => {
            switch (response) {
                case 200: this.options.icon = "info"; this.options.position = "top-end"; this.options.showConfirmButton = false; this.options.timer = 1500; this.options.title = "Item Updated"; this.alert_message(this.options); break; //Item Has been Updated 
                case 201: break; //Item Has been Created 
                case 301: this.options.icon = "info"; this.options.position = "top-end"; this.options.showConfirmButton = false; this.options.timer = 1500; this.options.title = "Item Updated"; this.alert_message(this.options); break; //Item Already in the basket

            }
            this.additem.ItemId = '';
        });
    }

    alert_message(options: AlertOptions) {

        Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Custom Title',
            showConfirmButton: true,
            timer: 2000
        });
    }


}
