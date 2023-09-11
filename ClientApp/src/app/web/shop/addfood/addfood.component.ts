import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Food } from 'src/app/interfaces/food';
import { ShopService } from 'src/app/services/shop.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-addfood',
  templateUrl: './addfood.component.html',
  styleUrls: ['./addfood.component.scss']
})
export class AddfoodComponent {
  addfood: FormGroup;


  constructor(private shop: ShopService, private fb: FormBuilder, private dialogRef: MatDialogRef<AddfoodComponent>) {
    this.addfood = this.fb.group({
      name: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]],
      description: [''],
      UserId: [''],
    });
  }
  add_food() {
    if (this.addfood.valid) {
      const formData = this.addfood.value;
      this.shop.Add_food(formData).subscribe({
        next: (result: Food) => {
          this.dialogRef.close();
          Swal.fire({
            position: 'top-end',
            icon: 'success',
            title: 'Food Has Been Added',
            showConfirmButton: false,
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
