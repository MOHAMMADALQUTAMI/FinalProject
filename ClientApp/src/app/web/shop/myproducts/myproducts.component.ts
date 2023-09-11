import { Component } from '@angular/core';
import { Food } from 'src/app/interfaces/food';
import { UserService } from 'src/app/services/user.service';
import { MatDialog } from '@angular/material/dialog';
import { ProductEditModalComponentComponent } from '../product-edit-modal-component/product-edit-modal-component.component';
import { AddfoodComponent } from '../addfood/addfood.component';
@Component({
  selector: 'app-myproducts',
  templateUrl: './myproducts.component.html',
  styleUrls: ['./myproducts.component.scss']
})
export class MyproductsComponent {
  Food: Food[] = [];

  constructor(private user: UserService, private dialog: MatDialog) {
    this.LoadFood();
  }

  openEditModal(product: Food): void {
    console.log('Product to edit:', product); // Add this line for debugging
    const dialogRef = this.dialog.open(ProductEditModalComponentComponent, {
      width: '300px',
      data: product,
    });



    dialogRef.afterClosed().subscribe(result => {
      // Handle any logic after the modal is closed, e.g., refresh the product list
      this.LoadFood();
    });
  }

  openAddModal(): void {
    const dialogRef = this.dialog.open(AddfoodComponent, {
      width: '300px',
    });
    dialogRef.afterClosed().subscribe(result => {
      // Handle any logic after the modal is closed, e.g., refresh the product list
      this.LoadFood();
    });
  }

  LoadFood() {
    this.user.GetOwnerFoods().subscribe((foods) => {
      console.log(foods);
      this.Food = foods;
    });
  }
}
