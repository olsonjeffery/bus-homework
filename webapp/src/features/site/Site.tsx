
import React, { useState } from 'react';

import { AppBar, Box, Tab, Tabs } from '@mui/material';
import Typography from '@mui/material/Typography';

import { useAppSelector, useAppDispatch } from '../../app/hooks';
import {
  setActiveTab,
  selectActiveTab,
} from './siteSlice';
import { store } from '../../app/store';

import { theme } from '../../theme-options';

import { Stop } from '../stop/Stop';

import { TabPanel, a11yProps } from './tab-panel';


const handleChange = (event: React.SyntheticEvent, newValue: number) => {
  store.dispatch(setActiveTab(newValue));
};

export function Site() {
  const activeTabValue: number = useAppSelector(selectActiveTab);

  return (
    <div className="site-container">
      <AppBar position="static">
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Bus Homework
          </Typography>
        <Tabs
          value={activeTabValue}
          onChange={handleChange}
          indicatorColor="secondary"
          textColor="inherit"
          variant="fullWidth"
          aria-label="full width tabs"
        >
          <Tab label="Default" {...a11yProps(0)} />
          <Tab label="Custom" {...a11yProps(1)} />
          <Tab label="Advanced" {...a11yProps(2)} />
        </Tabs>
      </AppBar>
      <TabPanel value={activeTabValue} index={0}>

      </TabPanel>
      <TabPanel value={activeTabValue} index={1}>
        Custom options go here
      </TabPanel>
      <TabPanel value={activeTabValue} index={2}>
        Advanced options go here
      </TabPanel>
      <Box sx={{ border: 1, borderColor: 'divider' }}>
        <Stop></Stop>
      </Box>
    </div>
  );
}