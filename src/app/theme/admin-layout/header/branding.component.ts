import { Component } from '@angular/core';

@Component({
  selector: 'app-branding',
  template: `
    <a class="matero-branding" href="#/">
      <img src="./assets/images/logo_dgh1_psd.gif" class="matero-branding-logo-expanded" alt="" />
      <span class="matero-branding-name"></span>
    </a>
  `,
})
export class BrandingComponent {}

