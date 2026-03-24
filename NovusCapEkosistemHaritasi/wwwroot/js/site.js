// Genel site JavaScript dosyası
document.addEventListener('DOMContentLoaded', function () {
    // İlk sayfa yüklendiğinde "Tümü" kategorisini seçili hale getir
    const allCategoriesButton = document.querySelector('.list-group-item[data-category="all"]');
    if (allCategoriesButton) {
        allCategoriesButton.classList.add('active');
    }
});

// Global JS işlevleri
document.addEventListener('DOMContentLoaded', function () {
    // Burada herhangi bir başlangıç işlevi tanımlanabilir
});

// Custom JavaScript for the application

// Wait for DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {
    // Initialize any components that need JavaScript
    initializeDropdowns();
    initializeModals();
    initializeTooltips();
    initializeAnimations();
    
    // Add fade-in animation to main content
    const mainContent = document.querySelector('main');
    if (mainContent) {
        mainContent.classList.add('fade-in');
    }
    
    // Add card hover effect to all cards
    const cards = document.querySelectorAll('.bg-white.rounded-lg.shadow-md');
    cards.forEach(card => {
        card.classList.add('card-hover');
    });
    
    // Add scale effect to action buttons
    const actionButtons = document.querySelectorAll('.bg-blue-600, .bg-red-600, .bg-green-600');
    actionButtons.forEach(button => {
        button.classList.add('hover-scale');
    });
});

// Dropdown functionality
function initializeDropdowns() {
    const dropdownToggles = document.querySelectorAll('[data-dropdown-toggle]');
    
    dropdownToggles.forEach(toggle => {
        toggle.addEventListener('click', function(e) {
            e.preventDefault();
            const targetId = this.getAttribute('data-dropdown-toggle');
            const target = document.getElementById(targetId);
            
            if (target) {
                target.classList.toggle('hidden');
            }
        });
    });
    
    // Close dropdowns when clicking outside
    document.addEventListener('click', function(e) {
        const dropdowns = document.querySelectorAll('.dropdown-menu:not(.hidden)');
        dropdowns.forEach(dropdown => {
            const isClickInside = dropdown.contains(e.target) || 
                                  e.target.getAttribute('data-dropdown-toggle') === dropdown.id;
            
            if (!isClickInside) {
                dropdown.classList.add('hidden');
            }
        });
    });
}

// Modal functionality
function initializeModals() {
    const modalToggles = document.querySelectorAll('[data-modal-toggle]');
    
    modalToggles.forEach(toggle => {
        toggle.addEventListener('click', function(e) {
            e.preventDefault();
            const targetId = this.getAttribute('data-modal-toggle');
            const target = document.getElementById(targetId);
            
            if (target) {
                target.classList.toggle('hidden');
            }
        });
    });
    
    // Close modal when clicking on backdrop
    const modalBackdrops = document.querySelectorAll('.modal-backdrop');
    modalBackdrops.forEach(backdrop => {
        backdrop.addEventListener('click', function(e) {
            if (e.target === this) {
                const modal = this.closest('.modal-container');
                if (modal) {
                    modal.classList.add('hidden');
                }
            }
        });
    });
    
    // Close modal with escape key
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape') {
            const visibleModals = document.querySelectorAll('.modal-container:not(.hidden)');
            visibleModals.forEach(modal => {
                modal.classList.add('hidden');
            });
        }
    });
}

// Tooltip functionality
function initializeTooltips() {
    const tooltips = document.querySelectorAll('[data-tooltip]');
    
    tooltips.forEach(tooltip => {
        const tooltipText = tooltip.getAttribute('data-tooltip');
        
        // Create tooltip element
        const tooltipElement = document.createElement('span');
        tooltipElement.className = 'tooltip-text';
        tooltipElement.textContent = tooltipText;
        
        // Add tooltip to element
        tooltip.classList.add('tooltip');
        tooltip.appendChild(tooltipElement);
    });
}

// Animation functionality
function initializeAnimations() {
    // Add animation to elements with data-animate attribute
    const animatedElements = document.querySelectorAll('[data-animate]');
    
    animatedElements.forEach(element => {
        const animationType = element.getAttribute('data-animate');
        
        // Add animation class based on type
        if (animationType === 'fade-in') {
            element.classList.add('fade-in');
        } else if (animationType === 'slide-in') {
            element.classList.add('slide-in');
        }
    });
}

// Form validation helper
function validateForm(formElement) {
    if (!formElement) return true;
    
    const inputs = formElement.querySelectorAll('input, select, textarea');
    let isValid = true;
    
    inputs.forEach(input => {
        // Check required fields
        if (input.hasAttribute('required') && (!input.value || !input.value.trim())) {
            markInvalid(input, 'Bu alan zorunludur.');
            isValid = false;
        }
        
        // Check email format
        if (input.type === 'email' && input.value && input.value.trim()) {
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(input.value.trim())) {
                markInvalid(input, 'Geçerli bir e-posta adresi giriniz.');
                isValid = false;
            }
        }
    });
    
    return isValid;
}

// Mark form field as invalid
function markInvalid(inputElement, message) {
    // Add error class
    inputElement.classList.add('border-red-500');
    
    // Check if error message already exists
    const parent = inputElement.parentElement;
    let errorElement = parent.querySelector('.error-message');
    
    if (!errorElement) {
        // Create error message element
        errorElement = document.createElement('p');
        errorElement.className = 'text-red-500 text-sm mt-1 error-message';
        parent.appendChild(errorElement);
    }
    
    // Set error message
    errorElement.textContent = message;
    
    // Clear error on input change
    inputElement.addEventListener('input', function() {
        this.classList.remove('border-red-500');
        const errorMsg = this.parentElement.querySelector('.error-message');
        if (errorMsg) {
            errorMsg.textContent = '';
        }
    }, { once: true });
}

// Helper function to format date
function formatDate(dateString) {
    const date = new Date(dateString);
    return date.toLocaleDateString('tr-TR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    });
}

// Helper function to truncate text
function truncateText(text, maxLength) {
    if (text.length <= maxLength) return text;
    return text.substr(0, maxLength) + '...';
} 