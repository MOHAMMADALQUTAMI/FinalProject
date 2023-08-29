import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/interfaces/Category';
import { AdminService } from 'src/app/services/admin.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { CreateSubcategory } from 'src/app/interfaces/createsubcategory';
import { SubCategory } from 'src/app/interfaces/subcategory';

@Component({
  selector: 'app-subcategory',
  templateUrl: './subcategory.component.html',
  styleUrls: ['./subcategory.component.scss']
})
export class SubcategoryComponent implements OnInit {

  categories: Category[] = [];
  subcategories: SubCategory[] = [];
  subcategory: FormGroup;

  constructor(private adminservice: AdminService, private formBuilder: FormBuilder) {
    this.subcategory = this.formBuilder.group({
      name: ['', Validators.required],
      categoryId: ['', Validators.required]
    });


  }

  ngOnInit() {
    this.loadCategories();
    this.loadSubCategories();
  }

  loadCategories() {
    this.adminservice.getCategories().subscribe((categories) => {
      this.categories = categories;
    });
  }

  loadSubCategories() {
    this.adminservice.getSubCategories().subscribe((result) => {
      console.log(result);
      this.subcategories = result;
    });
  }

  addSubCategory() {
    if (this.subcategory.valid) {
      const formData = this.subcategory.value;
      this.adminservice.addSubCategory(formData).subscribe(result => {
        console.log(result);
        this.loadSubCategories();
      });
    }
  }


}
