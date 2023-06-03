import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddModelToEventComponent } from './add-model-to-event.component';

describe('AddModelToEventComponent', () => {
  let component: AddModelToEventComponent;
  let fixture: ComponentFixture<AddModelToEventComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddModelToEventComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddModelToEventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
