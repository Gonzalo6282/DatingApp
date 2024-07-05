import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { authGuard } from './_guard/auth.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  //dummy route, pass all paths that need authGuard
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'lists', component: ListsComponent },
      { path: 'members', component: MemberListComponent },
      { path: 'members/:id', component: MemberDetailComponent },
      { path: 'messages', component: MessagesComponent },
    ],
  },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];
