import { Component } from '@angular/core';

@Component({
  selector: 'app-main-page',
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss']
})
export class MainPageComponent {
  products = [
    {
      title: 'Product 1',
      image: 'house_default.png',
      price: '$19.99'
    },
    {
      title: 'Product 2',
      image: 'house_default.png',
      price: '$29.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },
    {
      title: 'Product 3',
      image: 'house_default.png',
      price: '$9.99'
    },

  ];
}

