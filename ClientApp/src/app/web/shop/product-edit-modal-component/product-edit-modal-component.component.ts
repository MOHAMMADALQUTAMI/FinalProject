import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Food } from 'src/app/interfaces/food';
import { ShopService } from 'src/app/services/shop.service';

@Component({
  selector: 'app-product-edit-modal-component',
  templateUrl: './product-edit-modal-component.component.html',
  styleUrls: ['./product-edit-modal-component.component.scss']
})
export class ProductEditModalComponentComponent implements OnInit {
  FoodForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Food, private shop: ShopService, private formBuilder: FormBuilder) {
    this.FoodForm = this.formBuilder.group({
      Id: [data.id, Validators.required],
      name: [data.name, Validators.required],
      price: [data.price, Validators.required],
      description: [data.description, Validators.required],
    });
  }

  ngOnInit(): void {
  }

  saveChanges() {
    if (this.FoodForm.valid) {
      const formData = this.FoodForm.value;
      this.shop.Update_Food(formData).subscribe(result => {
        console.log(result);
      });
    }
  }

}
