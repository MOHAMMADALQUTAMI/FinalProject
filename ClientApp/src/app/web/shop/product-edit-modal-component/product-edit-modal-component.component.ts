import { HttpErrorResponse } from '@angular/common/http';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Food } from 'src/app/interfaces/food';
import { ShopService } from 'src/app/services/shop.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-product-edit-modal-component',
  templateUrl: './product-edit-modal-component.component.html',
  styleUrls: ['./product-edit-modal-component.component.scss']
})
export class ProductEditModalComponentComponent implements OnInit {
  FoodForm: FormGroup;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Food, private shop: ShopService, private formBuilder: FormBuilder, private dialogRef: MatDialogRef<ProductEditModalComponentComponent>) {
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
      this.shop.Update_Food(formData).subscribe({
        next: (result: any) => {
          this.dialogRef.close();
          Swal.fire({
            position: 'bottom-end',
            title: 'Food Has Been Updated',
            showConfirmButton: false,
            timerProgressBar: true,

            timer: 1500
          })
        },
        error: (error: HttpErrorResponse) => {
          // Handle the error based on the status code
          if (error.status === 400) {
            // Bad Request: Handle the error accordingly
          } else if (error.status === 401) {
            // Unauthorized: Handle the error accordingly
          } else {
            // Other HTTP error: Handle the error accordingly
          }
        },
      });
    }
  }

}
