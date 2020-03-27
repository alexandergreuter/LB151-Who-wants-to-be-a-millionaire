import unittest
from selenium import webdriver

class LBTestCase(unittest.TestCase):

    def setUp(self):
        self.browser = webdriver.Firefox()
        self.addCleanup(self.browser.quit)

    def testAdminLogin(self):
        self.browser.get('https://localhost:5001/admin')
        login = self.browser.find_element_by_id('mat-input-0')
        password = self.browser.find_element_by_id('mat-input-1')
        
        login.send_keys("admin@company.com")
        login.send_keys("password")
        
        self.browser.find_element_by_css_selector("button.mat-raised-button").click()
        
        try:
            self.browser.find_element_by_css_selector("h3")
        except NoSuchElementException:
            return False
        return True

if __name__ == '__main__':
    unittest.main(verbosity=2)

