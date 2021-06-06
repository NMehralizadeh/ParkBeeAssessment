import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GarageComponent } from './garage/garage.component';
import { MessageComponent } from './message/message.component';
import { LoginService } from './service/login.service';
import { environment } from 'src/environments/environment';
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  declarations: [AppComponent, GarageComponent, MessageComponent, NavbarComponent],
  imports: [BrowserModule, AppRoutingModule, FormsModule, HttpClientModule],
  providers: [LoginService],
  bootstrap: [AppComponent],
})
export class AppModule {
  constructor(private loginService: LoginService) {
    this.setToken();
  }

  setToken() {
    this.loginService
      .login('admin', 'admin')
      .subscribe((r) => (environment.token = r['token']));
  }
}
