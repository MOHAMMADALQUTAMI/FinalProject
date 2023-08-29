import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/interfaces/Category';
import { AdminService } from 'src/app/services/admin.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {
  categories: Category[] = [];
  categoryForm: FormGroup;

  constructor(private adminservice: AdminService, private formBuilder: FormBuilder) {
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required]
    });

    this.categoryForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
    });

  }

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.adminservice.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  addCategory() {
    if (this.categoryForm.valid) {
      const formData = this.categoryForm.value;
      this.adminservice.addCategory(formData).subscribe(() => {
        this.loadCategories();
      });
    }
  }

}
