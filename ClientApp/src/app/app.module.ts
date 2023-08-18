import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { HeaderComponent } from './main/header/header.component';
import { PagebodyComponent } from './main/pagebody/pagebody.component';
import { PagefooterComponent } from './main/pagefooter/pagefooter.component';
import { SidebarComponent } from './main/sidebar/sidebar.component';
import { SettingCustomizerComponent } from './main/setting-customizer/setting-customizer.component';
import { BasketComponent } from './main/basket/basket.component';
import { OrdersComponent } from './main/orders/orders.component';
import { ShopDashboardComponent } from './main/shop-dashboard/shop-dashboard.component';
import { ShopfoodsComponent } from './main/shopfoods/shopfoods.component';
import { CustomersComponent } from './main/customers/customers.component';
import { AuthGuard } from './guards/auth.guard';
import { FetchDataComponent } from './main/fetch-data/fetch-data.component';
import { fetchComponent } from './main/fetch-data/fetch.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NotfoundComponent,
    HeaderComponent,
    PagebodyComponent,
    PagefooterComponent,
    SidebarComponent,
    SettingCustomizerComponent,
    BasketComponent,
    OrdersComponent,
    ShopDashboardComponent,
    ShopfoodsComponent,
    CustomersComponent,
    FetchDataComponent,
    fetchComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent, },
      { path: 'register', component: RegisterComponent, },
      { path: '', component: PagebodyComponent, },
      {
        path: 'basket', component: BasketComponent,
      },
      { path: 'orders', canActivate: [AuthGuard], component: OrdersComponent, },
      { path: 'shopdashobard', canActivate: [AuthGuard], component: ShopDashboardComponent, },
      { path: 'shopfoods', canActivate: [AuthGuard], component: ShopfoodsComponent, },
      { path: 'customers', canActivate: [AuthGuard], component: CustomersComponent, },
      { path: '**', component: NotfoundComponent, },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
