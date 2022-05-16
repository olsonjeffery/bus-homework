import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import siteReducer from '../features/site/siteSlice';

export const store = configureStore({
  reducer: {
    site: siteReducer
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
