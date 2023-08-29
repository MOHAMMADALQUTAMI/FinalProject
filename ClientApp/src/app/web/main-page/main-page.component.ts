import { Component } from '@angular/core';
import { Food } from 'src/app/interfaces/food';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent {
  Food: Food[] = [];

  constructor(private user: UserService,) {
    this.LoadFood();
  }


  LoadFood() {
    this.user.GetFoods().subscribe((foods) => {
      this.Food = foods;
      console.log(foods);
    });
  }
}

